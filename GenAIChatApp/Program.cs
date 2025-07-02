using System;
using Azure;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

// Add references
using Azure.Identity;
using Azure.AI.Projects;
using Azure.AI.Inference;


class Program
{
    static void Main(string[] args)
    {
        // Clear the console
        Console.Clear();

        try
        {
            var endpoint = new Uri("https://jbao6-7779-resource.cognitiveservices.azure.com/openai/deployments/gpt-4o");
            var credential = new AzureKeyCredential("1b8hag9BB0WD3j8PXkWhfPZE4pR7c5YLqpAcCRIUqiW3TBmCrPTMJQQJ99BGACYeBjFXJ3w3AAAAACOGxuUJ");
            var model = "gpt-4o";

            var client = new ChatCompletionsClient(
                endpoint,
                credential,
                new AzureAIInferenceClientOptions()
            );

            // Initialize prompt with system message
            var prompt = new List<ChatRequestMessage>(){
                    new ChatRequestSystemMessage("You are a helpful AI assistant that answers questions.")
                };

            // Loop until the user types 'quit'
            string input_text = "";
            while (input_text.ToLower() != "quit")
            {
                // Get user input
                Console.WriteLine("Enter the prompt (or type 'quit' to exit):");
                input_text = Console.ReadLine();
                if (input_text.ToLower() != "quit")
                {
                    // Get a chat completion
                    prompt.Add(new ChatRequestUserMessage(input_text));
                    var requestOptions = new ChatCompletionsOptions()
                    {
                        Model = model,
                        Messages = prompt,
                        MaxTokens = 4096,
                        Temperature = 1.0f,
                        NucleusSamplingFactor = 1.0f
                    };

                    Response<ChatCompletions> response = client.Complete(requestOptions);
                    var completion = response.Value.Content;
                    Console.WriteLine(completion);
                    prompt.Add(new ChatRequestAssistantMessage(completion));

                }


            }



        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

