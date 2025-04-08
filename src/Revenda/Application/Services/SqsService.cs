using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
public class SqsService : ISqsService
{
    private readonly IAmazonSQS _sqsClient;
    private readonly string _queueUrl;
    private readonly ILogger<SqsService> _logger;

    public SqsService(
        IAmazonSQS sqsClient,
        IConfiguration config,
        ILogger<SqsService> logger)
    {
        _sqsClient = sqsClient;
        _queueUrl = Environment.GetEnvironmentVariable("AWS_SQS_QUEUE_URL");
        _logger = logger;
    }

    public async Task SendMessageAsync<T>(T message)
    {
        var request = new SendMessageRequest
        {
            QueueUrl = _queueUrl,
            MessageBody = JsonSerializer.Serialize(message)
        };

        await _sqsClient.SendMessageAsync(request);
        _logger.LogInformation($"Mensagem enviada para SQS: {typeof(T).Name}");
    }

    public async Task<List<Message>> ReceiveMessagesAsync()
    {
        var request = new ReceiveMessageRequest
        {
            QueueUrl = _queueUrl,
            MaxNumberOfMessages = 10,
            WaitTimeSeconds = 5
        };

        var response = await _sqsClient.ReceiveMessageAsync(request);
        return response.Messages;
    }

    public async Task DeleteMessageAsync(string receiptHandle)
    {
        await _sqsClient.DeleteMessageAsync(_queueUrl, receiptHandle);
    }
}