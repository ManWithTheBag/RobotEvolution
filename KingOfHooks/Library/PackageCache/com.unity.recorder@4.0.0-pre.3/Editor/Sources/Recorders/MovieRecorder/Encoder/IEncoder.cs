using Unity.Collections;
using UnityEditor.Media;
using UnityEngine;

namespace UnityEditor.Recorder.Encoder
{
    /// <summary>
    /// Interface to implement to create an encoder.
    /// </summary>
    public interface IEncoder
    {
        /// <summary>
        /// Opens the stream to add the audio and video frames to.
        /// </summary>
        /// <param name="settings">The settings of this encoder.</param>
        /// <param name="ctx">The context of the Recorder that drives this encoder.</param>
        void OpenStream(IEncoderSettings settings, RecordingContext ctx);

        /// <summary>
        /// Closes the stream of audio and video frames.
        /// </summary>
        void CloseStream();

        /// <summary>
        /// Encodes a Texture2D and adds it to the video stream.
        /// </summary>
        /// <param name="frame">The texture to encode.</param>
        /// <param name="time">The timestamp of the current frame.</param>
        void AddVideoFrame(Texture2D frame, MediaTime time)
        {
            AddVideoFrame(frame.GetPixelData<byte>(0), time);
        }

        /// <summary>
        /// Encodes an array of bytes and adds it to the video stream.
        /// </summary>
        /// <remarks>
        /// The encoder interprets the format of the array based on the texture format in IEncoderSettings passed to the OpenStream method.
        /// </remarks>
        /// <param name="bytes">The array of bytes to encode.</param>
        /// <param name="time">The timestamp of the current frame.</param>
        /// <seealso cref="UnityEditor.Recorder.Encoder.IEncoder.OpenStream"/>
        void AddVideoFrame(NativeArray<byte> bytes, MediaTime time);

        /// <summary>
        /// Encodes an array of audio values and adds it to the audio stream.
        /// </summary>
        /// <remarks>
        /// The number of channels is determined by the number of speakers in the scene.
        /// </remarks>
        /// <param name="bytes">The array of bytes to encode.</param>
        /// <seealso cref="UnityEditor.Recorder.Input.AudioInput"/>
        /// <seealso cref="UnityEngine.AudioSettings.speakerMode"/>
        void AddAudioFrame(NativeArray<float> bytes);
    }
}
