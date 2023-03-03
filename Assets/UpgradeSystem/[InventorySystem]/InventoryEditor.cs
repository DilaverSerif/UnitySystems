using System.Collections.Generic;
using System.IO;
using System.Linq;
using InventorySystem.Items;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace InventorySystem
{
    [ExecuteInEditMode]
    // ReSharper disable once RequiredBaseTypesIsNotInherited
    public class InventoryEditor: OdinMenuEditorWindow 
    {
        [MenuItem("Inventory/TestPanel")]
        private static void OpenWindow()
        {
            
            GetWindow<InventoryEditor>().Show();
        }
        
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            tree.Selection.SupportsMultiSelect = false;

            tree.Add("Utilities", new InventoryMenu());
            return tree;
        }
    }

    [CustomEditor(typeof(PlayerBag))]
    public class InventoryMenu
    {
        [InfoBox(@"errorMessage",InfoMessageType.Error)]
        [TableList]
        public List<Item> Items;

        private string ErrorMessage;
        
        public InventoryMenu()
        {
            Items = new List<Item>();
            
            var items = Resources.LoadAll<ItemData>($"Items").ToList();
            
            if (CheckIDs(ref items))
            {
                ErrorMessage = "There are duplicate IDs in the Items folder. Please fix this before continuing.";
                return;
            }
            else ErrorMessage = "";
            
            foreach (var item in items)
            {
                Items.Add(new Item(item));
            }
        }

        [Button]
        private void CreateEnums()
        {
            var itemNames = Items.Select(item => item.Name).ToArray();
            
            itemNames.CreateEnum("ItemNames","Items_");
        }
        
        [Button]
        private bool CheckIDs (ref List<ItemData> items)
        {
            if(items.Count == 0) 
                return false;
            var ids = items.Select(item => item.ID).ToList();

            var duplicates = ids.GroupBy(x => x).
                Count(group => group.Count() > 1);

            
            return duplicates > 0;
        }
    }

    
    public static class InventoryExtension
    {
        
#if UNITY_EDITOR
        public static string BasePath = "Assets/InventorySystem/EnumStorage/";

        public static void CreateEnum(this string[] itemsToEnum,string itemName,string title = "Enum_")
        {
            if (!Directory.Exists(BasePath))
            {
                Directory.CreateDirectory(BasePath);
            }

            var theItem = itemName + ".cs";
            var allPath = BasePath + theItem;

            var fileInside = $"public enum {title}" + itemName + "{ None,";
            if (itemsToEnum.Length > 0)
            {
            
                foreach (var item in itemsToEnum)
                {
                    var replace = item.Replace(" ", "_");

                    fileInside += " " + replace;
                    if (replace != itemsToEnum.Last())
                        fileInside += ",";
                }
                fileInside += "}";
            }
            else fileInside += "}";
            File.WriteAllText(allPath, fileInside);
            AssetDatabase.Refresh();

        }
#endif
        
    }
}
