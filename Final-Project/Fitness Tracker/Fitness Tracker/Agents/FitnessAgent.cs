using Microsoft.Extensions.AI;


namespace Fitness_Tracker.Agents
{
    public class FitnessAgent
    {
        private readonly IChatClient _chatClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public FitnessAgent(IChatClient chatClient, IHttpClientFactory httpClientFactory)
        {
            _chatClient = chatClient;
            _httpClientFactory = httpClientFactory;
        }   


        public async Task<string> AskAsync(string question)
        {
            var productData = await GetFitnessDataFromApiAsync();
              
            var messages = new List<ChatMessage>
            {
                new (ChatRole.System, $"You are a helpful fitness assistant that provides advice based on the user's data."),
                new (ChatRole.User,question)
            };


            var response = await _chatClient.GetResponseAsync(messages);
            return response.Text;
        }

        private async Task<string> GetFitnessDataFromApiAsync()
        {
            var client = _httpClientFactory.CreateClient("AgentApi");
            var response = await client.GetAsync("/api/cars");
            return await response.Content.ReadAsStringAsync();
        }
    }
}