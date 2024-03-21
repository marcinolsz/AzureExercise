namespace AzureExercise.Models
{
    public class Customer
    {
        public string Name { get; }
        public string Surname { get; }
        public int Age { get; }

        public Customer(string name, string surname, int age)
        {
            Name = name;
            Surname = surname;
            Age = age;
        }
    }
}
