using System;
using System.Threading.Tasks;
using OpenAI.Chat; // precisa instalar pacote oficial OpenAI
using System.Xml.Linq;

class Program
{
    static async Task Main()
    {
        string xml = @"<OTA_HotelResRQ>
            <HotelReservations>
                <HotelReservation>
                    <RoomStays>
                        <RoomStay>
                            <RoomTypes>
                                <RoomType RoomTypeCode='DLX'/>
                            </RoomTypes>
                            <RatePlans>
                                <RatePlan RatePlanCode='BAR'/>
                            </RatePlans>
                            <GuestCounts>
                                <GuestCount Count='2'/>
                            </GuestCounts>
                        </RoomStay>
                    </RoomStays>
                </HotelReservation>
            </HotelReservations>
        </OTA_HotelResRQ>";

        Console.WriteLine("Enviando XML para validação via IA...");

        // Configure sua API key no ambiente: export OPENAI_API_KEY=xxxx
        var client = new ChatClient(Environment.GetEnvironmentVariable("OPENAI_API_KEY")!);

        var response = await client.CompleteChatAsync(
            new ChatRequest
            {
                Model = "gpt-4o-mini",
                Messages =
                {
                    new Message(Role.System, "Você é um validador OTA (OpenTravel). Avalie XMLs de reserva."),
                    new Message(Role.User, $"Valide este XML OTA:\n{xml}")
                }
            });

        Console.WriteLine("Resposta da IA:");
        Console.WriteLine(response.ToString());
    }
}
