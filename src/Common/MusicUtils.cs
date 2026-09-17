using System;
using System.Drawing;
using System.IO;

namespace MusicAI.Common
{
    public static class MusicUtils
    {
        // 提取音乐文件的专辑图片
        public static Image GetAlbumArt(string filePath)
        {
            try
            {
                var file = TagLib.File.Create(filePath);
                if (file.Tag.Pictures.Length > 0)
                {
                    var bin = file.Tag.Pictures[0].Data.Data;
                    using (MemoryStream ms = new MemoryStream(bin))
                    {
                        var img = Image.FromStream(ms);
                        // 返回副本，避免多控件引用同一个Image实例导致冲突
                        return (Image)img.Clone();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("提取专辑图片失败: " + ex.Message);
            }
            return null;
        }
    }
}
