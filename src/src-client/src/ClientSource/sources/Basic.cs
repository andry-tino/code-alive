/// <summary>
/// Andrea Tino - 2018
/// </summary>

namespace CodeAlive.ClientSource.Basic
{
    using System;
    using System.Threading;

    using CodeAlive.Communication.RenderingApi;

    internal class SourceRunner : ISource
    {
        ICommunicationService svc;
        int pause; // In ms, 0 = no pause

        /// <summary>
        /// Creates a new instance of the <see cref="SourceRunner"/> class.
        /// </summary>
        /// <param name="svc"></param>
        /// <param name="pause"></param>
        public SourceRunner(ICommunicationService svc, int pause = 0)
        {
            this.svc = svc;
            this.pause = pause;
        }
        
        public void Run()
        {
            // Create an instance (the root object)
            //this.svc.Echo(new DiagnosticRenderingRequest() { Content = "Create an instance" });
            //this.svc.RenderNewCell(new NewInstanceRenderingRequest() { InstanceId = "root" });
            //this.Pause();

            // Create an instance
            var person = new Person();
            this.svc.Echo(new DiagnosticRenderingRequest() { Content = "Create an instance" });
            this.svc.RenderNewCell(new NewInstanceRenderingRequest() { InstanceId = "Person@person" });
            this.Pause();

            // Assign a property
            person.Name = "Claus";
            this.svc.Echo(new DiagnosticRenderingRequest() { Content = "Assign a property" });
            this.svc.RenderInteraction(new InteractionRenderingRequest() { InvocationName = "Person.Name", SourceInstanceId = "Cell", DstInstanceId = "Person@person" });
            this.Pause();

            // Assign a property
            person.Surname = "Valca";
            this.svc.Echo(new DiagnosticRenderingRequest() { Content = "Assign a property" });
            this.svc.RenderInteraction(new InteractionRenderingRequest() { InvocationName = "Person.Surname", SourceInstanceId = "Cell", DstInstanceId = "Person@person" });
            this.Pause();

            // Create an instance
            var piano = new Instrument();
            this.svc.Echo(new DiagnosticRenderingRequest() { Content = "Create an instance" });
            this.svc.RenderNewCell(new NewInstanceRenderingRequest() { InstanceId = "Instrument@piano" });
            this.Pause();

            // Assign a property
            piano.Name = "Classic Piano";
            this.svc.Echo(new DiagnosticRenderingRequest() { Content = "Assign a property" });
            this.svc.RenderInteraction(new InteractionRenderingRequest() { InvocationName = "Instrument.Name", SourceInstanceId = "Cell", DstInstanceId = "Instrument@piano" });
            this.Pause();

            // Create an instance
            var harmonica = new Instrument();
            this.svc.Echo(new DiagnosticRenderingRequest() { Content = "Create an instance" });
            this.svc.RenderNewCell(new NewInstanceRenderingRequest() { InstanceId = "Instrument@harmonica" });
            this.Pause();

            // Assign a property
            harmonica.Name = "Harmonica";
            this.svc.Echo(new DiagnosticRenderingRequest() { Content = "Assign a property" });
            this.svc.RenderInteraction(new InteractionRenderingRequest() { InvocationName = "Instrument.Name", SourceInstanceId = "Cell", DstInstanceId = "Instrument@harmonica" });
            this.Pause();

            // Assign a property
            person.Instrument1 = piano;
            this.svc.Echo(new DiagnosticRenderingRequest() { Content = "Assign a property" });
            this.svc.RenderReference(new GetReferenceRenderingRequest() { InstanceId = "Instrument@piano", ParentInstanceId = "Person@person" });
            this.svc.RenderInteraction(new InteractionRenderingRequest() { InvocationName = "Person.Instrument1", SourceInstanceId = "Instrument@piano", DstInstanceId = "Person@person" });
            this.Pause();

            // Assign a property
            person.Instrument2 = harmonica;
            this.svc.Echo(new DiagnosticRenderingRequest() { Content = "Assign a property" });
            this.svc.RenderReference(new GetReferenceRenderingRequest() { InstanceId = "Instrument@harmonica", ParentInstanceId = "Person@person" });
            this.svc.RenderInteraction(new InteractionRenderingRequest() { InvocationName = "Person.Instrument2", SourceInstanceId = "Instrument@harmonica", DstInstanceId = "Person@person" });
            this.Pause();

            // Execute a method returning a value
            var result = person.Play();
            this.svc.Echo(new DiagnosticRenderingRequest() { Content = "Execute a method returning a value" });
            this.svc.RenderInteraction(new InteractionRenderingRequest() { InvocationName = "Person.Play", SourceInstanceId = "Cell", DstInstanceId = "Person@person" });
            this.Pause();
        }

        public void RunOriginal()
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
            var result = person.Play();
        }

        private void Pause()
        {
            if (this.pause > 0)
            {
                Thread.Sleep(this.pause);
            }
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
