using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CygSoft.CodeCat.UI.Resources
{
    internal class ImageLibrary
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
                    imageList.Images.Add(upperKey, ImageFromIcon(icon));
            }
        }

        public void Add(string key, Image image)
        {
            string upperKey = key.ToUpper();

            if (!imageList.Images.ContainsKey(upperKey))
            {
                imageList.Images.Add(upperKey, image);
                if (!iconDictionary.ContainsKey(upperKey))
                    iconDictionary.Add(upperKey, IconFromImage(image));
            }
        }

        public void AddExtension(string fileExtensionKey)
        {
            string key = DotExtension(fileExtensionKey.ToUpper());
            
            if (!imageList.Images.ContainsKey(key))
            {
                Icon icon = IconByExtension(key);

                imageList.Images.Add(key, ImageFromIcon(icon));
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

        private Image ImageFromIcon(Icon icon)
        {
            return icon.ToBitmap();
        }

        public static Icon IconFromImage(Image image)
        {
            Bitmap bitmap = new Bitmap(image);
            IntPtr iconPtr = bitmap.GetHicon();
            Icon icon = Icon.FromHandle(iconPtr);

            return icon;
        }
    }
}
