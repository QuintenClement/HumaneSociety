using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public static class UserInterface
    {
        public static void DisplayUserOptions(List<string> options)
        {
            foreach(string option in options)
            {
                Console.WriteLine(option);
            }
        }

        public static void DisplayUserOptions(string options)
        {
            Console.WriteLine(options);
        }

        public static string GetUserInput()
        {
            string input = Console.ReadLine();
            switch (input.ToLower())
            {
                case "reset":
                    PointOfEntry.Run();
                    Environment.Exit(1);
                    break;
                case "exit":
                    Environment.Exit(1);
                    break;
                default:
                    break;
            }
            return input;
        }

        internal static void DisplayRoomInUse()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Console.Clear();
            Console.WriteLine("Rooms currently used:\n");
            var room = db.Rooms.Select(r => r).ToList();
            foreach (Room list in room)
            {
                Console.WriteLine("room Number: " + list.RoomNumber + " - In use by animal id: " + list.AnimalId);
            }
        }

        public static string GetStringData(string parameter, string target)
        {
            string data;
            DisplayUserOptions($"What is {target} {parameter}?");
            data = GetUserInput();
            return data;
        }

        internal static bool? GetBitData(List<string> options)
        {
            DisplayUserOptions(options);
            string input = GetUserInput();
            if (input.ToLower() == "yes" || input.ToLower() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool? GetBitData()
        {
            string input = GetUserInput();
            if (input.ToLower() == "yes" || input.ToLower() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        internal static bool? GetBitData(string target, string parameter)
        {
            DisplayUserOptions($"Is {target} {parameter}?");
            string input = GetUserInput();
            if (input.ToLower() == "yes" || input.ToLower() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static void DisplayAnimals(List<Animal> animals)
        {
            foreach(Animal animal in animals)
            {
                Console.WriteLine(animal.AnimalId + " " + animal.Name + " " + animal.Category.Name);
            }
        }

        internal static int GetIntegerData()
        {
            try
            {
                int data = int.Parse(GetUserInput());
                return data;
            }
            catch
            {
                Console.Clear();
                DisplayUserOptions("Incorrect input please enter an integer number.");
                return GetIntegerData();
            }
        }

        public static int GetIntegerData(string parameter, string target)
        {
            try
            {
                int data = int.Parse(GetStringData(parameter, target));
                return data;
            }
            catch
            {
                Console.Clear();
                DisplayUserOptions("Incorrect input please enter an integer number.");
                return GetIntegerData(parameter, target);
            }
        }

        internal static void DisplayClientInfo(Client client)
        {
            List<string> info = new List<string>() { client.FirstName, client.LastName, client.Email, client.Address.USState.Name };
            DisplayUserOptions(info);
            Console.ReadLine();
        }

        public static void DisplayAnimalInfo(Animal animal)
        {
            Room animalRoom = Query.GetRoom(animal.AnimalId);
            List<string> info = new List<string>() {"ID: " + animal.AnimalId, animal.Name, animal.Age + " years old", "Demeanor: " + animal.Demeanor, "Kid friendly: " + BoolToYesNo(animal.KidFriendly), "pet friendly: " + BoolToYesNo(animal.PetFriendly), $"Room Number: " + animalRoom.RoomNumber, "Weight: " + animal.Weight.ToString(),  "Food amoumnt in cups:" + animal.DietPlan.FoodAmountInCups};
            DisplayUserOptions(info);
            Console.ReadLine();

        }

        private static string BoolToYesNo(bool? input)
        {
            if (input == true)
            {
                return "yes";
            }
            else
            {
                return "no";
            }
        }

        public static bool GetBitData(string option)
        {
            DisplayUserOptions(option);
            string input = GetUserInput();
            if (input.ToLower() == "yes" || input.ToLower() == "y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Dictionary<int, string> GetAnimalCriteria()
        {
            Dictionary<int, string> searchParameters = new Dictionary<int, string>();
            bool isSearching = true;
            while (isSearching)
            {
                Console.Clear();
                List<string> options = new List<string>() { "Select Search Criteia: (Enter number and choose finished when finished)", "1. Category", "2. Name", "3. Age", "4. Demeanor", "5. Kid friendly", "6. Pet friendly", "7. Weight", "8. ID", "9. Finished" };
                DisplayUserOptions(options);
                string input = GetUserInput();
                if (input.ToLower() == "10" || input.ToLower() == "finished")
                {
                    isSearching = false;
                    continue;
                }
                else
                {
                    searchParameters = EnterSearchCriteria(searchParameters, input);
                }
            }
            return searchParameters;
        }
        public static Dictionary<int, string> EnterSearchCriteria(Dictionary<int, string> searchParameters, string input)
        {
            Console.Clear();
            switch (input)
            {
                case "1":
                    searchParameters.Add(1, UserInterface.GetStringData("category", "the animal's"));
                    return searchParameters;
                case "2":
                    searchParameters.Add(2, UserInterface.GetStringData("name", "the animal's"));
                    return searchParameters;
                case "3":
                    searchParameters.Add(3, UserInterface.GetIntegerData("age", "the animal's").ToString());
                    return searchParameters;
                case "4":
                    searchParameters.Add(4, UserInterface.GetStringData("demeanor", "the animal's"));
                    return searchParameters;
                case "5":
                    searchParameters.Add(5, UserInterface.GetBitData("the animal", "kid friendly").ToString());
                    return searchParameters;
                case "6":
                    searchParameters.Add(6, UserInterface.GetBitData("the animal", "pet friendly").ToString());
                    return searchParameters;
                case "7":
                    searchParameters.Add(7, UserInterface.GetIntegerData("weight", "the animal's").ToString());
                    return searchParameters;
                case "8":
                    searchParameters.Add(8, UserInterface.GetIntegerData("ID", "the animal's").ToString());
                    return searchParameters;
                default:
                    UserInterface.DisplayUserOptions("Input not recognized please try agian");
                    return searchParameters;
            }
        }
        internal static void DisplayCategory()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Console.Clear();
            Console.WriteLine("Current Category List:\n");
            var category = db.Categories.Select(c => c).ToList();
            {
                foreach (Category list in category)
                {
                    Console.WriteLine(list.Name);
                }
            }
        }

        internal static void DisplayDietPlan()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            Console.Clear();
            Console.WriteLine("Current Diet Plans Avaiable:\n");
            var dietPlan = db.DietPlans.Select(d => d).ToList();
            {
                foreach (DietPlan list in dietPlan)
                {
                    Console.WriteLine(list.Name);
                }
            }
        }
    }
}
