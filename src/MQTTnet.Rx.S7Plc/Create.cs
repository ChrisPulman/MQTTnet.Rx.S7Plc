// Copyright (c) Chris Pulman. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Reactive.Linq;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;
using MQTTnet.Rx.Client;
using S7PlcRx;

namespace MQTTnet.Rx.S7Plc
{
    /// <summary>
    /// Create.
    /// </summary>
    public static class Create
    {
        /// <summary>
        /// Publishes the serial port.
        /// </summary>
        /// <typeparam name="T">The PLC Tag Data Type.</typeparam>
        /// <param name="client">The client.</param>
        /// <param name="topic">The publish topic.</param>
        /// <param name="plcVariable">The variable.</param>
        /// <param name="configurePlc">The configure S7PLC.</param>
        /// <returns>
        /// MqttClientPublishResult.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">
        /// client
        /// or
        /// configureS7plc
        /// or
        /// s7plc.
        /// </exception>
        public static IObservable<MqttClientPublishResult> PublishS7PlcTag<T>(this IObservable<IMqttClient> client, string topic, string plcVariable, Action<IRxS7> configurePlc)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (configurePlc == null)
            {
                throw new ArgumentNullException(nameof(configurePlc));
            }

            var s7plc = default(IRxS7)!;
            configurePlc(s7plc);
            if (s7plc == null)
            {
                throw new ArgumentNullException(nameof(s7plc));
            }

            return client.PublishMessage(s7plc.Observe<T>(plcVariable).Select(payLoad => (topic, payLoad!.ToString())));
        }

        /// <summary>
        /// Publishes the s7 PLC tag.
        /// </summary>
        /// <typeparam name="T">The PLC Tag Data Type.</typeparam>
        /// <param name="client">The client.</param>
        /// <param name="topic">The publish topic.</param>
        /// <param name="plcVariable">The PLC variable.</param>
        /// <param name="configurePlc">The configure S7PLC.</param>
        /// <returns>A ApplicationMessageProcessedEventArgs.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// client
        /// or
        /// configureS7plc
        /// or
        /// s7plc.
        /// </exception>
        public static IObservable<ApplicationMessageProcessedEventArgs> PublishS7PlcTag<T>(this IObservable<IManagedMqttClient> client, string topic, string plcVariable, Action<IRxS7> configurePlc)
        {
            if (client == null)
            {
                throw new ArgumentNullException(nameof(client));
            }

            if (configurePlc == null)
            {
                throw new ArgumentNullException(nameof(configurePlc));
            }

            var s7plc = default(IRxS7)!;
            configurePlc(s7plc);
            if (s7plc == null)
            {
                throw new ArgumentNullException(nameof(s7plc));
            }

            return client.PublishMessage(s7plc.Observe<T>(plcVariable).Select(payLoad => (topic, payLoad!.ToString())));
        }
    }
}
