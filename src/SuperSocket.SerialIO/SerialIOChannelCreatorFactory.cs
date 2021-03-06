using System;
using System.Buffers;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SuperSocket.Channel;
using SuperSocket.ProtoBase;

namespace SuperSocket.SerialIO
{
    public class SerialIOChannelCreatorFactory : IChannelCreatorFactory
    {
        public SerialIOChannelCreatorFactory(IServiceProvider serviceProvider)
        {

        }

        public IChannelCreator CreateChannelCreator<TPackageInfo>(ListenOptions options, ChannelOptions channelOptions, ILoggerFactory loggerFactory, object pipelineFilterFactory)
        {
            var filterFactory = pipelineFilterFactory as IPipelineFilterFactory<TPackageInfo>;
            channelOptions.Logger = loggerFactory.CreateLogger(nameof(IChannel));
            var channelFactoryLogger = loggerFactory.CreateLogger(nameof(SerialIOChannelCreator));

            return new SerialIOChannelCreator(options, (s) =>
                new ValueTask<IChannel>(new SerialIOPipeChannel<TPackageInfo>(s, filterFactory.Create(s), channelOptions)), channelFactoryLogger);
        }
    }
}