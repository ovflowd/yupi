using System;
using DotNetty.Codecs;
using DotNetty.Transport.Channels;
using DotNetty.Buffers;
using System.Collections.Generic;

namespace Yupi.Net.DotNettyImpl
{
	public class HeaderDecoder : ByteToMessageDecoder
	{
		/// <summary>
		/// Decode the header of a message if available
		/// </summary>
		/// <param name="context">The socket context</param>
		/// <param name="input">The input buffer.</param>
		/// <param name="output">List containing decoded messages (if any)</param>
		protected override void Decode (IChannelHandlerContext context, IByteBuffer input, List<object> output)
		{
			if (input.ReadableBytes < 4) {
				return;
			}

			// Mark is used by ResetReaderIndex
			input.MarkReaderIndex ();

			// It is possible to call ReadInt here since it decodes the int as big-endian (network order)
			int length = input.ReadInt();

			if (input.ReadableBytes < length) {
				input.ResetReaderIndex ();
				return;
			}

			output.Add (input.ReadBytes (length));
		}
	}
}

