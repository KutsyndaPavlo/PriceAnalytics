﻿global using Microsoft.Extensions.Logging;
global using Polly;
global using Polly.Retry;
global using RabbitMQ.Client;
global using RabbitMQ.Client.Events;
global using RabbitMQ.Client.Exceptions;
global using System;
global using System.IO;
global using System.Net.Sockets;
global using Autofac;
global using PriceAnalytics.Infrustructure.EventBus;
global using PriceAnalytics.Infrustructure.EventBus.Abstractions;
global using PriceAnalytics.Infrustructure.EventBus.Events;
global using PriceAnalytics.Infrustructure.EventBus.Extensions;
global using System.Text;
global using System.Threading.Tasks;
global using System.Text.Json;
