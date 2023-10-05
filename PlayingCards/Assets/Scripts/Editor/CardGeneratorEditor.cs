using System;
using System.IO;
using Data;
using Enums;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Editor
{
    [CustomEditor(typeof(CardGenerator))]
    public class CardGeneratorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var cardGenerator = (CardGenerator) target;
            
            DrawDefaultInspector();
            DrawGenerateButton(cardGenerator);
        }

        private static void DrawGenerateButton(CardGenerator cardGenerator)
        {
            if (GUILayout.Button("Generate"))
            {
                var directoryPath = GetDirectoryPath(cardGenerator.Folder);

                foreach (CardSuit cardSuit in Enum.GetValues(typeof(CardSuit)))
                foreach (CardValue cardValue in Enum.GetValues(typeof(CardValue)))
                {
                    GenerateCard(cardValue, cardSuit, directoryPath, cardGenerator.DataStorage);
                }
            }
        }

        private static string GetDirectoryPath(Object folderToSafe)
        {
            var path = AssetDatabase.GetAssetPath(folderToSafe);

            if (string.IsNullOrEmpty(path))
            {
                if (!Directory.Exists(Const.DefaultPath))
                {
                    AssetDatabase.CreateFolder(Const.MainFolderName, Const.FolderName);
                }

                path = Const.DefaultPath;
            }

            return path;
        }

        private static void GenerateCard(CardValue cardValue, CardSuit cardSuit, string directoryPath, DataStorage dataStorage)
        {
            var cardName = $"{cardValue}Of{cardSuit}";
            var cardPath = directoryPath + $"/{cardName}.prefab";
            var card = new GameObject(cardName);
            var cardColor = dataStorage.CardColorData[cardSuit];
            var color = dataStorage.CardColorData[cardColor];
            var valueSprite = dataStorage.CardValueData[cardValue];
            var cardBodyColor = dataStorage.CardColorData[dataStorage.CardColorData.CardBodyColor];
            var borderColor = dataStorage.CardColorData[dataStorage.CardColorData.BorderColor];
            var suitPositions = dataStorage.CardSuitPositionData[cardValue];
            var suitSprite = dataStorage.CardSuitData[cardSuit];
            
            var cardBody = Instantiate(dataStorage.CardComponentsData.CardBody, Vector3.zero, Quaternion.identity, card.transform);
            cardBody.UpdateView(cardBodyColor, borderColor);
            
            foreach (var cardValuePosition in dataStorage.CardValuePositionData.CardValuePositions)
            {
                var value = Instantiate(dataStorage.CardComponentsData.SpriteView, Const.Offset + cardValuePosition.ToVector3(), Quaternion.identity, card.transform);
                value.UpdateView(valueSprite, color);
            }

            foreach (var suitPosition in suitPositions)
            {
                var suit = Instantiate(dataStorage.CardComponentsData.SpriteView, Const.Offset + suitPosition.ToVector3(), Quaternion.identity, card.transform);
                suit.UpdateView(suitSprite, color);
            }

            if (dataStorage.HighRankData.GetIsHighRank(cardValue))
            {
                var rankSprite = dataStorage.HighRankData[cardValue];
                var rank = Instantiate(dataStorage.CardComponentsData.SpriteView, Const.Offset + dataStorage.HighRankData.HighRankSpritePosition.ToVector3(), Quaternion.identity, card.transform);
                rank.UpdateView(rankSprite, color);
            }

            PrefabUtility.SaveAsPrefabAsset(card, cardPath);
            AssetDatabase.Refresh();
            DestroyImmediate(card);
        }
    }
}