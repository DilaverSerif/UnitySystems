using System.Collections.Generic;
using System.IO;
using System.Linq;
using InventorySystem;
using InventorySystem.Items;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace UpgradeSystem._InventorySystem_
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
            // tree.Add("Look Inventory", new InventoryShower());
            return tree;
        }
    }
    
    public class InventoryMenu
    {
        [TableList]
        public List<Item> Items;
        
        public InventoryMenu()
        {
            Items = new List<Item>();
            
            var items = UnityEngine.Resources.LoadAll<ItemData>($"Items").ToList();
            

            foreach (var item in items)
            {
                Items.Add(new Item(item));
            }
        }

        [Button]
        public void CreateEnums()
        {
            var itemNames = Items.Select(item => item.name).ToArray();
            
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
        
        private static string BasePath = "Assets/InventorySystem/EnumStorage/";

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
        
    }
}
