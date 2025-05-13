/*
 * @author: Kylan Frittelli (ST10438112)
 * @created: 02/05/2025
 * @function: BlobStorageService class for handling file uploads to Azure Blob Storage (CLDV6211 POE Part 2)
 */

using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using EventManagerMVC.Services;

//-------namespace--------//
namespace EventManagerMVC.Services
{
    //-------------BlobStorageService class----------------//
    public class BlobStorageService : IBlobStorageService
    {
        private readonly string _connectionString;
        private readonly string _containerName;

        //constructor----------------//
        public BlobStorageService(IConfiguration config)
        {
            _connectionString = config["AzureStorage:ConnectionString"]; //connection string from appsettings.json
            _containerName = config["AzureStorage:ContainerName"]; //container name from appsettings.json
        }

        //UploadFileBlobAsync method----------------//
        public async Task<string> UploadFileBlobAsync(Stream fileStream, string fileName, string containerName)
        {
            var containerClient = new BlobContainerClient(_connectionString, containerName);
            await containerClient.CreateIfNotExistsAsync();

            var uniqueName = Guid.NewGuid().ToString() + Path.GetExtension(fileName);
            var blobClient = containerClient.GetBlobClient(uniqueName);

            await blobClient.UploadAsync(fileStream);

            return blobClient.Uri.ToString();
        }

        //-----------------------------//
    }
    //-----------------------------//
}
//----------------------------------//
//END OF FILE>>>>>>>>>>>>>>>>>>>>>>>>>>>

/* Refrences:
 * Huawei Technologies, 2023. Cloud Computing Technologies. Hangzhou: Posts & Telecom Press.
 * Mrzyglód, K., 2022. Azure for Developers. 2nd ed. Birmingham: Packt Publishing.
 * Microsoft Corporation, 2022. The Developer’s Guide to Azure. Redmond: Microsoft Press.
 * OpenAI, 2025. ChatGPT. [online] Available at: https://openai.com/chatgpt/ [Accessed 02 May 2025].
 * Github Inc., 2025. GitHub Copilot. [online] Available at: https://github.com [Accessed 02 May 2025].
 * Varsity College, 2025. INSY6112 Module Manual. Cape Town: The Independent Institute of Education.
 */
