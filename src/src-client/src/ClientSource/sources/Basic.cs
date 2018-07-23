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
            // Create an instance (the root object)
            this.svc.Echo(new DiagnosticRenderingRequest() { Content = "Create an instance" });
            this.svc.RenderNewCell(new NewInstanceRenderingRequest() { InstanceId = "root" });

            // Create an instance
            var person = new Person();
            this.svc.Echo(new DiagnosticRenderingRequest() { Content = "Create an instance" });
            this.svc.RenderNewCell(new NewInstanceRenderingRequest() { InstanceId = "Person@person" });

            // Assign a property
            person.Name = "Claus";
            this.svc.Echo(new DiagnosticRenderingRequest() { Content = "Assign a property" });
            this.svc.RenderInteraction(new InteractionRenderingRequest() { InvocationName = "Person.Name", SourceInstanceId = "root", DstInstanceId = "Person@person" });

            // Assign a property
            person.Surname = "Valca";
            this.svc.Echo(new DiagnosticRenderingRequest() { Content = "Assign a property" });
            this.svc.RenderInteraction(new InteractionRenderingRequest() { InvocationName = "Person.Surname", SourceInstanceId = "root", DstInstanceId = "Person@person" });

            // Create an instance
            var piano = new Instrument();
            this.svc.Echo(new DiagnosticRenderingRequest() { Content = "Create an instance" });
            this.svc.RenderNewCell(new NewInstanceRenderingRequest() { InstanceId = "Instrument@piano" });

            // Assign a property
            piano.Name = "Classic Piano";
            this.svc.Echo(new DiagnosticRenderingRequest() { Content = "Assign a property" });
            this.svc.RenderInteraction(new InteractionRenderingRequest() { InvocationName = "Instrument.Name", SourceInstanceId = "root", DstInstanceId = "Instrument@piano" });

            // Create an instance
            var harmonica = new Instrument();
            this.svc.Echo(new DiagnosticRenderingRequest() { Content = "Create an instance" });
            this.svc.RenderNewCell(new NewInstanceRenderingRequest() { InstanceId = "Instrument@harmonica" });

            // Assign a property
            harmonica.Name = "Harmonica";
            this.svc.Echo(new DiagnosticRenderingRequest() { Content = "Assign a property" });
            this.svc.RenderInteraction(new InteractionRenderingRequest() { InvocationName = "Instrument.Name", SourceInstanceId = "root", DstInstanceId = "Instrument@harmonica" });

            // Assign a property
            person.Instrument1 = piano;
            this.svc.Echo(new DiagnosticRenderingRequest() { Content = "Assign a property" });
            this.svc.RenderInteraction(new InteractionRenderingRequest() { InvocationName = "Person.Instrument1", SourceInstanceId = "root", DstInstanceId = "Person@person" });

            // Assign a property
            person.Instrument2 = harmonica;
            this.svc.Echo(new DiagnosticRenderingRequest() { Content = "Assign a property" });
            this.svc.RenderInteraction(new InteractionRenderingRequest() { InvocationName = "Person.Instrument2", SourceInstanceId = "root", DstInstanceId = "Person@person" });

            // Execute a method returning a value
            var result = person.Play();
            this.svc.Echo(new DiagnosticRenderingRequest() { Content = "Execute a method returning a value" });
            this.svc.RenderInteraction(new InteractionRenderingRequest() { InvocationName = "Person.Play", SourceInstanceId = "root", DstInstanceId = "Person@person" });
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
