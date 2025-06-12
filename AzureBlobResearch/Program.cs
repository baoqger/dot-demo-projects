using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Specialized;
using System.Text;

// 连接字符串和容器/Blob名称
string connectionString = "DefaultEndpointsProtocol=https;AccountName=chrisbaotest;AccountKey=+vaFA0XDa1MJ68ArUdEwjjgHHlBufGxZe5f0i3pepOllZaiFelTT4xD3J8qoGr+dR/tJNrkwKRLc+AStINfEig==;EndpointSuffix=core.windows.net";
string containerName = "timeseries";
string blobName = "sensor-data-2023.log";

// 创建Blob服务客户端
var blobServiceClient = new BlobServiceClient(connectionString);

// 获取容器引用(如果不存在则创建)
var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
await containerClient.CreateIfNotExistsAsync();

// 获取追加Blob引用
var appendBlobClient = containerClient.GetAppendBlobClient(blobName);

// 如果Blob不存在则创建
if (!await appendBlobClient.ExistsAsync())
{
    await appendBlobClient.CreateAsync();
}

// 模拟时间序列数据
var timestamp = DateTime.UtcNow;
var value = 23.5;
var data = $"{timestamp:o},{value}\n";

// 追加数据到Blob
using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
{
    await appendBlobClient.AppendBlockAsync(stream);
}