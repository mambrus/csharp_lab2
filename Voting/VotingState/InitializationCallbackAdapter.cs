// This adapter is needed to inject the custom serializer into the StateManager
// instance which can only be done while initializing VotingState’s base constructor.
// The OnInitialize is called to create the custom serializer and add it to the StateManager
// using the TryAddStateSerializer method.You’ll notice that this method is marked obsolete,
// which is why the OnInitialize method is also marked obsolete.This is the supported mechanism
// for custom serialization, but this API will change in the future.

using Microsoft.ServiceFabric.Data;
using System;
using System.Threading.Tasks;

namespace VotingState
{
    // Enables configuration of the state manager.
    public sealed class InitializationCallbackAdapter
    {
        [Obsolete("This method uses a method that is marked as obsolete.", false)]
        public Task OnInitialize()
        {
            // This is marked obsolete, but is supported. This interface is likely
            // to change in the future.
            var serializer = new VotingDataSerializer();
            this.StateManager.TryAddStateSerializer(serializer);
            return Task.FromResult(true);
        }

        public IReliableStateManager StateManager { get; set; }
    }
}