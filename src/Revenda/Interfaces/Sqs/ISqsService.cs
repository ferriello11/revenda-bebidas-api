using Amazon.SQS.Model;

public interface ISqsService
{
    Task SendMessageAsync<T>(T message);
    Task<List<Message>> ReceiveMessagesAsync();
    Task DeleteMessageAsync(string receiptHandle);
}