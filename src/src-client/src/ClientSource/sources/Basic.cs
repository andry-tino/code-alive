/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace CodeAlive.ClientSource.Basic
{
    using System;

    using CodeAlive.Communication.RenderingApi;

    internal class SourceRunner : ISource
    {
        ICommunicationService svc;

        public SourceRunner(ICommunicationService svc)
        {
            this.svc = svc;
        }

        /// <summary>
        /// Runs the program.
        /// </summary>
        public void Run()
        {
            // Create an instance
            var person = new Person();
            this.svc.Echo("Create an instance");

            // Assign a property
            person.Name = "Claus";
            this.svc.Echo("Assign a property");

            // Assign a property
            person.Surname = "Valca";
            this.svc.Echo("Assign a property");

            // Create an instance
            var piano = new Instrument();
            this.svc.Echo("Create an instance");

            // Assign a property
            piano.Name = "Classic Piano";
            this.svc.Echo("Assign a property");

            // Create an instance
            var harmonica = new Instrument();
            this.svc.Echo("Create an instance");

            // Assign a property
            harmonica.Name = "Harmonica";
            this.svc.Echo("Assign a property");

            // Assign a property
            person.Instrument1 = piano;
            this.svc.Echo("Assign a property");

            // Assign a property
            person.Instrument2 = harmonica;
            this.svc.Echo("Assign a property");

            // Execute a method returning a value
            var result = person.Play();
            this.svc.Echo("Execute a method returning a value");
        }

        #region Types

        /// <summary>
        /// Represents a person in this program.
        /// </summary>
        private class Person
        {
            public string Name { get; set; }

            public string Surname { get; set; }

            public Instrument Instrument1 { get; set; }

            public Instrument Instrument2 { get; set; }

            public string Play()
            {
                var result = "";

                if (this.Instrument1 != null)
                {
                    result += this.Instrument1.Play();
                }

                if (this.Instrument2 != null)
                {
                    result += this.Instrument2.Play();
                }

                return result;
            }
        }

        /// <summary>
        /// Represents an instrument in this program.
        /// </summary>
        private class Instrument
        {
            public string Name { get; set; }

            public string Play()
            {
                return $"{this.Name}: Ding Ding Dong";
            }
        }

        #endregion
    }
}
