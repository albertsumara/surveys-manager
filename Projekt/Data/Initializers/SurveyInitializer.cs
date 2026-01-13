using Projekt.Models;
using Microsoft.EntityFrameworkCore;

namespace Projekt.Data.Initializers;

public static class SurveyInitializer
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        var context = services.GetRequiredService<ProjektContext>();

        if (await context.Surveys.AnyAsync())
            return; // już istnieją ankiety

        // Lista ankiet
        var surveys = new List<Survey>
        {
            new Survey
            {
                Title = "Preferencje sportowe",
                Questions = new List<Question>
                {
                    new Question
                    {
                        Content = "Jaki sport uprawiasz najczęściej?",
                        Answers = new List<Answer>
                        {
                            new Answer { Content = "Piłka nożna" },
                            new Answer { Content = "Koszykówka" },
                            new Answer { Content = "Siatkówka" },
                            new Answer { Content = "Żaden" }
                        }
                    },
                    new Question
                    {
                        Content = "Ile razy w tygodniu ćwiczysz?",
                        Answers = new List<Answer>
                        {
                            new Answer { Content = "0" },
                            new Answer { Content = "1-2" },
                            new Answer { Content = "3-4" },
                            new Answer { Content = "5+" }
                        }
                    }
                }
            },

            new Survey
            {
                Title = "Nawyki żywieniowe",
                Questions = new List<Question>
                {
                    new Question
                    {
                        Content = "Jak często jesz fast food?",
                        Answers = new List<Answer>
                        {
                            new Answer { Content = "Codziennie" },
                            new Answer { Content = "Kilka razy w tygodniu" },
                            new Answer { Content = "Raz w miesiącu" },
                            new Answer { Content = "Nigdy" }
                        }
                    },
                    new Question
                    {
                        Content = "Ile porcji warzyw jesz dziennie?",
                        Answers = new List<Answer>
                        {
                            new Answer { Content = "0-1" },
                            new Answer { Content = "2-3" },
                            new Answer { Content = "4-5" },
                            new Answer { Content = "6+" }
                        }
                    }
                }
            },

            new Survey
            {
                Title = "Technologia i media",
                Questions = new List<Question>
                {
                    new Question
                    {
                        Content = "Jak często korzystasz z mediów społecznościowych?",
                        Answers = new List<Answer>
                        {
                            new Answer { Content = "Codziennie" },
                            new Answer { Content = "Kilka razy w tygodniu" },
                            new Answer { Content = "Raz w miesiącu" },
                            new Answer { Content = "Nigdy" }
                        }
                    },
                    new Question
                    {
                        Content = "Jak długo dziennie spędzasz przed ekranem?",
                        Answers = new List<Answer>
                        {
                            new Answer { Content = "<1h" },
                            new Answer { Content = "1-3h" },
                            new Answer { Content = "4-6h" },
                            new Answer { Content = "7h+" }
                        }
                    }
                }
            },

            new Survey
            {
                Title = "Styl życia",
                Questions = new List<Question>
                {
                    new Question
                    {
                        Content = "Czy palisz papierosy?",
                        Answers = new List<Answer>
                        {
                            new Answer { Content = "Tak" },
                            new Answer { Content = "Nie" }
                        }
                    },
                    new Question
                    {
                        Content = "Czy pijesz alkohol?",
                        Answers = new List<Answer>
                        {
                            new Answer { Content = "Codziennie" },
                            new Answer { Content = "Kilka razy w tygodniu" },
                            new Answer { Content = "Raz w miesiącu" },
                            new Answer { Content = "Nie piję" }
                        }
                    }
                }
            },

            new Survey
            {
                Title = "Podróże",
                Questions = new List<Question>
                {
                    new Question
                    {
                        Content = "Jak często podróżujesz?",
                        Answers = new List<Answer>
                        {
                            new Answer { Content = "Raz w miesiącu" },
                            new Answer { Content = "Kilka razy w roku" },
                            new Answer { Content = "Raz w roku" },
                            new Answer { Content = "Nigdy" }
                        }
                    },
                    new Question
                    {
                        Content = "Preferujesz podróże:",
                        Answers = new List<Answer>
                        {
                            new Answer { Content = "Samolotem" },
                            new Answer { Content = "Samochodem" },
                            new Answer { Content = "Pociągiem" },
                            new Answer { Content = "Autostop" }
                        }
                    }
                }
            },

            new Survey
            {
                Title = "Zakupy i konsumpcja",
                Questions = new List<Question>
                {
                    new Question
                    {
                        Content = "Jak często robisz zakupy online?",
                        Answers = new List<Answer>
                        {
                            new Answer { Content = "Codziennie" },
                            new Answer { Content = "Kilka razy w tygodniu" },
                            new Answer { Content = "Raz w miesiącu" },
                            new Answer { Content = "Nigdy" }
                        }
                    },
                    new Question
                    {
                        Content = "Jak często korzystasz z promocji?",
                        Answers = new List<Answer>
                        {
                            new Answer { Content = "Zawsze" },
                            new Answer { Content = "Często" },
                            new Answer { Content = "Rzadko" },
                            new Answer { Content = "Nigdy" }
                        }
                    }
                }
            },

            new Survey
            {
                Title = "Kultura i rozrywka",
                Questions = new List<Question>
                {
                    new Question
                    {
                        Content = "Jak często chodzisz do kina?",
                        Answers = new List<Answer>
                        {
                            new Answer { Content = "Codziennie" },
                            new Answer { Content = "Raz w tygodniu" },
                            new Answer { Content = "Raz w miesiącu" },
                            new Answer { Content = "Nigdy" }
                        }
                    },
                    new Question
                    {
                        Content = "Jakiego typu muzyki słuchasz najczęściej?",
                        Answers = new List<Answer>
                        {
                            new Answer { Content = "Pop" },
                            new Answer { Content = "Rock" },
                            new Answer { Content = "Hip-hop" },
                            new Answer { Content = "Klasyczna" }
                        }
                    }
                }
            },

            new Survey
            {
                Title = "Edukacja",
                Questions = new List<Question>
                {
                    new Question
                    {
                        Content = "Jaki poziom wykształcenia posiadasz?",
                        Answers = new List<Answer>
                        {
                            new Answer { Content = "Podstawowe" },
                            new Answer { Content = "Średnie" },
                            new Answer { Content = "Wyższe" },
                            new Answer { Content = "Doktorat" }
                        }
                    },
                    new Question
                    {
                        Content = "Czy uczysz się języków obcych?",
                        Answers = new List<Answer>
                        {
                            new Answer { Content = "Tak, regularnie" },
                            new Answer { Content = "Czasami" },
                            new Answer { Content = "Nie" }
                        }
                    }
                }
            },

            new Survey
            {
                Title = "Zdrowie psychiczne",
                Questions = new List<Question>
                {
                    new Question
                    {
                        Content = "Jak często czujesz się zestresowany?",
                        Answers = new List<Answer>
                        {
                            new Answer { Content = "Codziennie" },
                            new Answer { Content = "Kilka razy w tygodniu" },
                            new Answer { Content = "Raz w miesiącu" },
                            new Answer { Content = "Nigdy" }
                        }
                    },
                    new Question
                    {
                        Content = "Czy medytujesz lub relaksujesz się?",
                        Answers = new List<Answer>
                        {
                            new Answer { Content = "Codziennie" },
                            new Answer { Content = "Kilka razy w tygodniu" },
                            new Answer { Content = "Raz w miesiącu" },
                            new Answer { Content = "Nigdy" }
                        }
                    }
                }
            },

            new Survey
            {
                Title = "Technologie i bezpieczeństwo",
                Questions = new List<Question>
                {
                    new Question
                    {
                        Content = "Czy używasz menadżera haseł?",
                        Answers = new List<Answer>
                        {
                            new Answer { Content = "Tak" },
                            new Answer { Content = "Nie" }
                        }
                    },
                    new Question
                    {
                        Content = "Czy regularnie aktualizujesz oprogramowanie?",
                        Answers = new List<Answer>
                        {
                            new Answer { Content = "Tak" },
                            new Answer { Content = "Czasami" },
                            new Answer { Content = "Nigdy" }
                        }
                    }
                }
            }
        };

        context.Surveys.AddRange(surveys);
        await context.SaveChangesAsync();
    }
}
