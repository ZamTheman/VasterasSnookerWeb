using System;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Threading.Tasks;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Image = System.Drawing.Image;
using System.Collections.Generic;

namespace VästeråsSnooker.DB
{
    public class FileStorageDA
    {
        private static readonly CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
        private static readonly CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
        private static readonly CloudBlobContainer container = blobClient.GetContainerReference("imagecontainer");

        public static bool StoreImage(Image img, ImageFormat format, string name)
        {
            try
            {
                container.CreateIfNotExists();
                container.SetPermissions(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Container
                });
                CloudBlockBlob blob = container.GetBlockBlobReference(name);

                using (MemoryStream stream = new MemoryStream())
                {
                    img.Save(stream, format);
                    blob.Properties.ContentType = format.ToString();
                    blob.UploadFromStream(stream);
                }
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public static async Task<bool> UploadImage(Image img, string imageName)
        {
            
            var blob = container.GetBlockBlobReference(imageName);
            try
            {
                using(MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, ImageFormat.Png);
                    ms.Seek(0, SeekOrigin.Begin);
                    await blob.UploadFromStreamAsync(ms);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static async Task<string> GetImageByPlayerId(int playerId)
        {
            await container.SetPermissionsAsync(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Container
            });
            
            IEnumerable<IListBlobItem> allProfileBlobs = container.ListBlobs(null, false);
            foreach (CloudBlockBlob uri in allProfileBlobs)
            {
                if(uri.Name.Contains(playerId + "_profileImage"))
                {
                    return uri.Uri.ToString();
                }
            }
            return null;
        }
    }
}