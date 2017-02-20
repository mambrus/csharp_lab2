// This is a custom serializer for the VotingData type. We’re showing how to do custom serialization because 
// the default serializer, the DataContractSerializer, is a bit slow and custom serialization is one of the
// best practices for increasing stateful service performance.

using Microsoft.ServiceFabric.Data;
using System.IO;
using Bond;
using Bond.Protocols;
using Bond.IO.Unsafe;

namespace VotingState
{
    // Custom serializer for the VotingData structure.
    public sealed class VotingDataSerializer : IStateSerializer<VotingData>
    {
        private readonly static Serializer<CompactBinaryWriter<OutputBuffer>> Serializer;
        private readonly static Deserializer<CompactBinaryReader<InputBuffer>> Deserializer;

        static VotingDataSerializer()
        {
            // Create the serializers and deserializers for FileMetadata.
            Serializer = new Serializer<CompactBinaryWriter<OutputBuffer>>(typeof(VotingData));
            Deserializer = new Deserializer<CompactBinaryReader<InputBuffer>>(typeof(VotingData));
        }

        public VotingDataSerializer()
        {

        }

        public VotingData Read(BinaryReader binaryReader)
        {
            int count = binaryReader.ReadInt32();
            byte[] bytes = binaryReader.ReadBytes(count);

            var input = new InputBuffer(bytes);
            var reader = new CompactBinaryReader<InputBuffer>(input);
            return Deserializer.Deserialize<VotingData>(reader);
        }

        public VotingData Read(VotingData baseValue, BinaryReader binaryReader)
        {
            return Read(binaryReader);
        }

        public void Write(VotingData value, BinaryWriter binaryWriter)
        {
            var output = new OutputBuffer();
            var writer = new CompactBinaryWriter<OutputBuffer>(output);
            Serializer.Serialize(value, writer);

            binaryWriter.Write(output.Data.Count);
            binaryWriter.Write(output.Data.Array, output.Data.Offset, output.Data.Count);
        }

        public void Write(VotingData baseValue, VotingData targetValue, BinaryWriter binaryWriter)
        {
            Write(targetValue, binaryWriter);
        }
    }
}

// +-------------------------+---+--------+--------+--------+----------+
// |       Class Type        |   | normal | static | sealed | abstract |
// +-------------------------+---+--------+--------+--------+----------+
// | Can be instantiated     | : | YES    | NO     | YES    | NO       |
// | Can be inherited        | : | YES    | NO     | NO     | YES      |
// | Can inherit from others | : | YES    | NO     | YES    | YES      |
// +-------------------------+---+--------+--------+--------+----------+