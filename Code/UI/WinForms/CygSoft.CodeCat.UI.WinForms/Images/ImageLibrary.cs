﻿using CygSoft.CodeCat.UI.WinForms.UiHelpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.WinForms.Images
{
    public class ImageLibrary
    {
        private static ImageList imageList = new ImageList();
        private static Dictionary<string, Icon> iconDictionary = new Dictionary<string, Icon>();

        public ImageList ImageList { get { return imageList; } }

        public int GetImageIndex(string key)
        {
            return imageList.Images.IndexOfKey(key);
        }

        public void Add(string key, Icon icon)
        {
            string upperKey = key.ToUpper();

            if (!iconDictionary.ContainsKey(upperKey))
            {
                iconDictionary.Add(upperKey, icon);
                if (!imageList.Images.ContainsKey(upperKey))
                    imageList.Images.Add(upperKey, Gui.Drawing.ImageFromIcon(icon));
            }
        }

        public void Add(string key, Image image)
        {
            string upperKey = key.ToUpper();

            if (!imageList.Images.ContainsKey(upperKey))
            {
                imageList.Images.Add(upperKey, image);
                if (!iconDictionary.ContainsKey(upperKey))
                    iconDictionary.Add(upperKey, Gui.Drawing.IconFromImage(image));
            }
        }

        public void AddExtension(string fileExtensionKey)
        {
            string key = DotExtension(fileExtensionKey.ToUpper());
            
            if (!imageList.Images.ContainsKey(key))
            {
                Icon icon = IconByExtension(key);

                imageList.Images.Add(key, Gui.Drawing.ImageFromIcon(icon));
                if (!iconDictionary.ContainsKey(key))
                    iconDictionary.Add(key, icon);
            }
        }

        public void AddExtensions(string[] extensions)
        {
            foreach (string extension in extensions)
                AddExtension(extension);
        }

        public ImageOutput this[string key, bool isFileExtensionKey = false]
        {
            get
            {
                Image image = null;
                Icon icon = null;

                if (isFileExtensionKey)
                {
                    string extension = DotExtension(key.ToUpper());

                    if (imageList.Images.ContainsKey(extension))
                        image = imageList.Images[extension];
                    else
                    {
                        image = ImageByExtension(extension);
                        AddExtension(extension);
                    }

                    if (iconDictionary.ContainsKey(extension))
                        icon = iconDictionary[extension];

                    return new ImageOutput(icon, image, imageList.Images.IndexOfKey(extension), extension);
                }
                else
                {
                    string upperKey = key.ToUpper();

                    if (imageList.Images.ContainsKey(upperKey))
                        image = imageList.Images[upperKey];

                    if (iconDictionary.ContainsKey(upperKey))
                        icon = iconDictionary[upperKey];

                    return new ImageOutput(icon, image, imageList.Images.IndexOfKey(upperKey), upperKey);
                }
            }
        }

        public void Clear()
        {
            iconDictionary.Clear();
            imageList.Images.Clear();
        }


        private Image ImageByExtension(string extension)
        {
            Icon icon = Etier.IconHelper.IconReader.GetFileIcon(DotExtension(extension),
            Etier.IconHelper.IconReader.IconSize.Small,
            false);

            return icon.ToBitmap();
        }

        public Icon IconByExtension(string extension)
        {
            Icon icon = Etier.IconHelper.IconReader.GetFileIcon(DotExtension(extension),
            Etier.IconHelper.IconReader.IconSize.Small,
            false);

            return icon;
        }

        private static string DotExtension(string extension)
        {
            if (extension.StartsWith("."))
                return extension;
            else
                return "." + extension;
        }
    }
}
