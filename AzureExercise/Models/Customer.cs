namespace AzureExercise.Models
{
    public record Customer
    {
        public string Name { get; init; }
        public string Surname { get; init; }
        public int Age { get; init; }

        public Customer(string name, string surname, int age)
        {
            Name = name;
            Surname = surname;
            Age = age;
        }
    }
}
