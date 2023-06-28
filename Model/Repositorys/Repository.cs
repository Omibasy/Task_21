using Task_20.Model.Data;


namespace Task_20.Model.Repositorys
{
    public static class Repository
    {
        public static List<Person> GetPersonalities()
        {
            List<Person> personalities = new List<Person>();

            personalities.Add(new Person()
            {
                ID = 1,
                Name = "Андрей",
                Surname = "Голубев",
                Patomic = "Сергеевич",
            });
            personalities.Add(new Person()
            {
                ID = 2,
                Name = "Кристина",
                Surname = "Боброва",
                Patomic = "Андреевна",
            });
            personalities.Add(new Person()
            {
                ID = 3,
                Name = "Татьяна",
                Surname = "Ершова",
                Patomic = "Павловна",
            });
            personalities.Add(new Person()
            {
                ID = 4,
                Name = "Юлия",
                Surname = "Михайлова",
                Patomic = "Артуровна",
            });
            personalities.Add(new Person()
            {
                ID = 5,
                Name = "Владимир",
                Surname = "Маслов",
                Patomic = "Маркович",
            });

            return personalities;
        }

        public static List<PersonalData> GetPersonalData()
        {
            List<PersonalData> personalDatas = new List<PersonalData>();

            personalDatas.Add(new PersonalData()
            {
                ID = 1,
                ID_Person = 1,
                PhoneNumber = GetPhoneNumber(1),
                Address = "Новосибирская область, город Ступино, пл. Балканская,19",
                Description = GetText(),
            });
            personalDatas.Add(new PersonalData()
            {
                ID = 2,
                ID_Person = 2,
                PhoneNumber = GetPhoneNumber(2),
                Address = "Калининградская область, город Серебряные Пруды, проезд Ленина, 82",
                Description = GetText(),
            });
            personalDatas.Add(new PersonalData()
            {
                ID = 3,
                ID_Person = 3,
                PhoneNumber = GetPhoneNumber(3),
                Address = "Новгородская область, город Ступино, бульвар Ладыгина, 98",
                Description = GetText(),
            });
            personalDatas.Add(new PersonalData()
            {
                ID = 4,
                ID_Person = 4,
                PhoneNumber = GetPhoneNumber(4),
                Address = "Кировская область, город Павловский Посад, спуск Славы, 60",
                Description = GetText(),
            });
            personalDatas.Add(new PersonalData()
            {
                ID = 5,
                ID_Person = 5,
                PhoneNumber = GetPhoneNumber(5),
                Address = "Мурманская область, город Егорьевск, пл. Гагарина, 35",
                Description = GetText(),
            });


            return personalDatas;
        }

        private static string GetText()
        {
            return " Lorem ipsum dolor sit amet consectetur, adipisicing elit. Molestias ducimus dignissimos optio,\r\ndolores cupiditate culpa eveniet placeat nihil, deleniti ab dicta? Illum et at possimus aliquid";
        }

        private static string GetPhoneNumber(int id)
        {
            if (id > 9)
            {
                Random random = new Random();
                id = random.Next(0, 9);

            }

            return $"8-{id}{id}{id}-{id}{id}{id}-{id}{id}-{id}{id}";

        }
    }
}
