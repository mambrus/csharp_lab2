﻿using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace VotingState
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class VotingState : StatefulService, IVotingService
    {
        public VotingState(StatefulServiceContext context)
            : base(context)
        { }

        public VotingState(StatefulServiceContext context, InitializationCallbackAdapter adapter)
            : base(
                context,
                new ReliableStateManager(
                    context,
                    new ReliableStateManagerConfiguration(
                        onInitializeStateSerializersEvent: adapter.OnInitialize)
                )
            )
        {
            adapter.StateManager = this.StateManager;
        }

        public long RequestCount
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public Task AddVoteAsync(string key, int count, string id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<VotingData>> GetVotingDataAsync(string id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) 
        /// for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return new ServiceReplicaListener[]
            {
                new ServiceReplicaListener(serviceContext => new OwinCommunicationListener(
                    serviceContext,
                    this,
                    ServiceEventSource.Current,
                    "ServiceEndpoint"))
            };
        }

        /// <summary>
        /// This is the main entry point for your service replica.
        /// This method executes when this replica of your service becomes primary and has write status.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service replica.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
    }
}
