/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace CodeAlive.ClientSource.Basic
{
    internal class SourceRunner : ISource
    {
        public void Run()
        {
            var person = new Person();

            person.Name = "Claus";
            person.Surname = "Valca";

            var piano = new Instrument();
            piano.Name = "Classic Piano";

            var harmonica = new Instrument();
            harmonica.Name = "Harmonica";

            person.Instrument1 = piano;
            person.Instrument2 = harmonica;

            person.Play();
        }
    }

    /// <summary>
    /// Represents a person in this program.
    /// </summary>
    internal class Person
    {
        public Person()
        {
        }

        public string Name { get; set; }

        public string Surname { get; set; }

        public Instrument Instrument1 { get; set; }

        public Instrument Instrument2 { get; set; }

        public void Play()
        {
            if (this.Instrument1 != null)
            {
                this.Instrument1.Play();
            }

            if (this.Instrument2 != null)
            {
                this.Instrument2.Play();
            }
        }
    }

    /// <summary>
    /// Represents an instrument in this program.
    /// </summary>
    internal class Instrument
    {
        public Instrument()
        {
        }

        public string Name { get; set; }

        public string Play()
        {
            return $"{this.Name}: Ding Ding Dong";
        }
    }
}
