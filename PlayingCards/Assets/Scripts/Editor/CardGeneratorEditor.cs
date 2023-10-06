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
                var cardBodyColor = GetCardBodyColor(dataStorage);
                var cardBorderColor = GetCardBorderColor(dataStorage);
                
                foreach (CardSuit cardSuit in Enum.GetValues(typeof(CardSuit)))
                foreach (CardValue cardValue in Enum.GetValues(typeof(CardValue)))
                {
                    CalculateParameters(dataStorage, cardValue, cardSuit, directoryPath, 
                        out var cardName, out var cardPrefabPath, out var valueSprite, out var suitSprite, out var suitPositions, out var color);
            
                    var cardPrefab = new GameObject(cardName);
            
                    var cardBody = Instantiate(dataStorage.CardComponentsData.CardBodyView, Vector3.zero, Quaternion.identity, cardPrefab.transform);
                    cardBody.UpdateView(cardBodyColor, cardBorderColor);
            
                    foreach (var cardValuePosition in dataStorage.CardValuePositionData.CardValuePositions)
                    {
                        var value = Instantiate(dataStorage.CardComponentsData.ValueView, Offset + cardValuePosition.ToVector3(), Quaternion.identity, cardPrefab.transform);
                        value.SpriteView.UpdateView(valueSprite, color);
                    }

                    foreach (var suitPosition in suitPositions)
                    {
                        var suit = Instantiate(dataStorage.CardComponentsData.SuitView, Offset + suitPosition.ToVector3(), Quaternion.identity, cardPrefab.transform);
                        suit.SpriteView.UpdateView(suitSprite, color);
                    }

                    if (dataStorage.HighRankData.GetIsHighRank(cardValue))
                    {
                        var rankSprite = dataStorage.HighRankData[cardValue];
                        var rank = Instantiate(dataStorage.CardComponentsData.HighRankView, Offset + dataStorage.HighRankData.HighRankSpritePosition.ToVector3(), Quaternion.identity, cardPrefab.transform);
                        rank.SpriteView.UpdateView(rankSprite, color);
                    }

                    PrefabUtility.SaveAsPrefabAsset(cardPrefab, cardPrefabPath);
                    AssetDatabase.Refresh();
                    DestroyImmediate(cardPrefab);
                }

                var prefabPath = GetPrefabPath(directoryPath, CardBackData.CardBackName);
                var cardBackPrefab = new GameObject(CardBackData.CardBackName);

                var cardBodyView = Instantiate(dataStorage.CardComponentsData.CardBodyView, Vector3.zero, Quaternion.identity, cardBackPrefab.transform);
                cardBodyView.UpdateView(cardBodyColor, cardBorderColor);
                
                var cardBackView = Instantiate(dataStorage.CardComponentsData.CardBackView, Offset, Quaternion.identity, cardBackPrefab.transform);
                cardBackView.SpriteView.UpdateView(dataStorage.CardBackData.CardBackSprite, dataStorage.CardBackData.CardBackColor);
                
                PrefabUtility.SaveAsPrefabAsset(cardBackPrefab, prefabPath);
                AssetDatabase.Refresh();
                DestroyImmediate(cardBackPrefab);
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
                var cardBodyColor = GetCardBodyColor(dataStorage);
                var cardBorderColor = GetCardBorderColor(dataStorage);

                foreach (CardSuit cardSuit in Enum.GetValues(typeof(CardSuit)))
                foreach (CardValue cardValue in Enum.GetValues(typeof(CardValue)))
                {
                    CalculateParameters(dataStorage, cardValue, cardSuit, directoryPath, 
                        out _, out var cardPrefabPath, out var valueSprite, out var suitSprite, out var suitPositions, out var color);
                    
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

                {
                    var prefabPath = GetPrefabPath(directoryPath, CardBackData.CardBackName);
                    var cardBackPrefab = AssetDatabase.LoadAssetAtPath(prefabPath, typeof(GameObject)) as GameObject;
                    CardGeneratorExceptionHandler.CheckObjectNull(cardBackPrefab);
                
                    // ReSharper disable once PossibleNullReferenceException
                    var cardBodyViews = cardBackPrefab.GetComponentsInChildren<CardBodyView>();

                    foreach (var cardBodyView in cardBodyViews)
                    {
                        cardBodyView.UpdateView(cardBodyColor, cardBorderColor);
                    }
                    
                    var cardBackViews = cardBackPrefab.GetComponentsInChildren<CardBackView>();

                    foreach (var cardBackView in cardBackViews)
                    {
                        cardBackView.SpriteView.UpdateView(dataStorage.CardBackData.CardBackSprite, dataStorage.CardBackData.CardBackColor);
                    }
                    
                    PrefabUtility.SavePrefabAsset(cardBackPrefab);
                }
            }
        }

        private static string GetDirectoryPath(CardGenerator cardGenerator) => AssetDatabase.GetAssetPath(cardGenerator.TargetFolder);

        private static void CalculateParameters(DataStorage dataStorage, CardValue cardValue, CardSuit cardSuit, string cardsDirectoryPath, 
            out string cardName, out string cardPrefabPath, out Sprite valueSprite, out Sprite suitSprite, out IReadOnlyList<Vector2> suitPositions, out Color color)
        {
            cardName = $"{cardValue}Of{cardSuit}";
            cardPrefabPath = GetPrefabPath(cardsDirectoryPath, cardName);
            valueSprite = dataStorage.CardValueData[cardValue];
            suitSprite = dataStorage.CardSuitData[cardSuit];
            suitPositions = dataStorage.CardSuitPositionData[cardValue];
            color = dataStorage.CardColorData[dataStorage.CardColorData[cardSuit]];
        }

        private static string GetPrefabPath(string directoryPath, string prefabName) => $"{directoryPath}/{prefabName}{PrefabExtension}";
        private static Color GetCardBodyColor(DataStorage dataStorage) => dataStorage.CardColorData[dataStorage.CardColorData.CardBodyColor];
        private static Color GetCardBorderColor(DataStorage dataStorage) => dataStorage.CardColorData[dataStorage.CardColorData.BorderColor];
    }
}