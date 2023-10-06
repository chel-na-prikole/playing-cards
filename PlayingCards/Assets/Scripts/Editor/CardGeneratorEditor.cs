using System;
using System.Collections.Generic;
using Data;
using Enums;
using UnityEditor;
using UnityEngine;
using Views;

namespace Editor
{
    [CustomEditor(typeof(CardGenerator))]
    public class CardGeneratorEditor : UnityEditor.Editor
    {
        private static readonly Vector3 Offset = new(0f, 0f, -0.01f);
        private const string PrefabExtension = ".prefab";

        public override void OnInspectorGUI()
        {
            var cardGenerator = (CardGenerator) target;
            
            DrawDefaultInspector();
            DrawGenerateButton(cardGenerator);
            DrawUpdateCardsButton(cardGenerator);
        }

        private static void DrawGenerateButton(CardGenerator cardGenerator)
        {
            if (GUILayout.Button("Generate"))
            {
                CardGeneratorExceptionHandler.CheckFolderPath(cardGenerator);
                
                var directoryPath = GetDirectoryPath(cardGenerator);
                var dataStorage = cardGenerator.DataStorage;
                
                foreach (CardSuit cardSuit in Enum.GetValues(typeof(CardSuit)))
                foreach (CardValue cardValue in Enum.GetValues(typeof(CardValue)))
                {
                    CalculateParameters(dataStorage, cardValue, cardSuit, directoryPath, 
                        out var cardName, out var cardPrefabPath, out var valueSprite, out var suitSprite, 
                        out var suitPositions, out var color, out var cardBodyColor, out var cardBorderColor);
            
                    var card = new GameObject(cardName);
            
                    var cardBody = Instantiate(dataStorage.CardComponentsData.CardBody, Vector3.zero, Quaternion.identity, card.transform);
                    cardBody.UpdateView(cardBodyColor, cardBorderColor);
            
                    foreach (var cardValuePosition in dataStorage.CardValuePositionData.CardValuePositions)
                    {
                        var value = Instantiate(dataStorage.CardComponentsData.ValueView, Offset + cardValuePosition.ToVector3(), Quaternion.identity, card.transform);
                        value.SpriteView.UpdateView(valueSprite, color);
                    }

                    foreach (var suitPosition in suitPositions)
                    {
                        var suit = Instantiate(dataStorage.CardComponentsData.SuitView, Offset + suitPosition.ToVector3(), Quaternion.identity, card.transform);
                        suit.SpriteView.UpdateView(suitSprite, color);
                    }

                    if (dataStorage.HighRankData.GetIsHighRank(cardValue))
                    {
                        var rankSprite = dataStorage.HighRankData[cardValue];
                        var rank = Instantiate(dataStorage.CardComponentsData.HighRankView, Offset + dataStorage.HighRankData.HighRankSpritePosition.ToVector3(), Quaternion.identity, card.transform);
                        rank.SpriteView.UpdateView(rankSprite, color);
                    }

                    PrefabUtility.SaveAsPrefabAsset(card, cardPrefabPath);
                    AssetDatabase.Refresh();
                    DestroyImmediate(card);
                }
            }
        }

        private static void DrawUpdateCardsButton(CardGenerator cardGenerator)
        {
            if (GUILayout.Button("Update cards"))
            {
                CardGeneratorExceptionHandler.CheckFolderPath(cardGenerator);
                var directoryPath = GetDirectoryPath(cardGenerator);
                var dataStorage = cardGenerator.DataStorage;
                var valuePositions = dataStorage.CardValuePositionData.CardValuePositions;

                foreach (CardSuit cardSuit in Enum.GetValues(typeof(CardSuit)))
                foreach (CardValue cardValue in Enum.GetValues(typeof(CardValue)))
                {
                    CalculateParameters(dataStorage, cardValue, cardSuit, directoryPath, 
                        out _, out var cardPrefabPath, out var valueSprite, out var suitSprite, 
                        out var suitPositions, out var color, out var cardBodyColor, out var cardBorderColor);
                    
                    var prefabObject = AssetDatabase.LoadAssetAtPath(cardPrefabPath, typeof(GameObject)) as GameObject;
                    CardGeneratorExceptionHandler.CheckObjectNull(prefabObject);

                    // ReSharper disable once PossibleNullReferenceException
                    var cardBodyViews = prefabObject.GetComponentsInChildren<CardBodyView>();

                    foreach (var cardBodyView in cardBodyViews)
                    {
                        cardBodyView.UpdateView(cardBodyColor, cardBorderColor);
                    }
                    
                    var valueViews = prefabObject.GetComponentsInChildren<ValueView>();
                    CardGeneratorExceptionHandler.CheckCollectionsCountEquality(valuePositions, valueViews);

                    for (var i = 0; i < valueViews.Length; i++)
                    {
                        var valueView = valueViews[i];
                        var valuePosition = valuePositions[i];
                        
                        valueView.SetPosition(valuePosition);
                        valueView.SpriteView.UpdateView(valueSprite, color);
                    }

                    var suitViews = prefabObject.GetComponentsInChildren<SuitView>();
                    CardGeneratorExceptionHandler.CheckCollectionsCountEquality(suitPositions, suitViews);

                    for (var i = 0; i < suitViews.Length; i++)
                    {
                        var suitView = suitViews[i];
                        var suitPosition = suitPositions[i];

                        suitView.SetPosition(suitPosition);
                        suitView.SpriteView.UpdateView(suitSprite, color);
                    }
                    
                    PrefabUtility.SavePrefabAsset(prefabObject);
                }
            }
        }

        private static string GetDirectoryPath(CardGenerator cardGenerator) => AssetDatabase.GetAssetPath(cardGenerator.TargetFolder);

        private static void CalculateParameters(DataStorage dataStorage, CardValue cardValue, CardSuit cardSuit, string cardsDirectoryPath, 
            out string cardName, out string cardPrefabPath, out Sprite valueSprite, out Sprite suitSprite, 
            out IReadOnlyList<Vector2> suitPositions, out Color color, out Color cardBodyColor, out Color cardBorderColor)
        {
            cardName = $"{cardValue}Of{cardSuit}";
            cardPrefabPath = $"{cardsDirectoryPath}/{cardName}{PrefabExtension}";
            valueSprite = dataStorage.CardValueData[cardValue];
            suitSprite = dataStorage.CardSuitData[cardSuit];
            suitPositions = dataStorage.CardSuitPositionData[cardValue];
            color = dataStorage.CardColorData[dataStorage.CardColorData[cardSuit]];
            cardBodyColor = dataStorage.CardColorData[dataStorage.CardColorData.CardBodyColor];
            cardBorderColor = dataStorage.CardColorData[dataStorage.CardColorData.BorderColor];
        }
    }
}