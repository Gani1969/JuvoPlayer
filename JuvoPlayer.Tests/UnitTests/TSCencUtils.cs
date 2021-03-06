/*!
 *
 * [https://github.com/SamsungDForum/JuvoPlayer])
 * Copyright 2019, Samsung Electronics Co., Ltd
 * Licensed under the MIT license
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *
 */

using JuvoPlayer.Drms.Cenc;
using NUnit.Framework;

namespace JuvoPlayer.Tests.UnitTests
{
    [TestFixture]
    public class TSCencUtils
    {
        [Test]
        public void SchemeIdUriToSystemId_PlayReadySchemeId_ReturnsSystemId()
        {
            const string schemeId = "urn:uuid:9a04f079-9840-4286-ab92-e65be0885f95";

            var expected = new byte[]
            {
                0x9a, 0x04, 0xf0, 0x79, 0x98, 0x40, 0x42, 0x86,
                0xab, 0x92, 0xe6, 0x5b, 0xe0, 0x88, 0x5f, 0x95,
            };

            var received = CencUtils.SchemeIdUriToSystemId(schemeId);

            Assert.That(received, Is.EqualTo(expected));
        }

        [Test]
        public void SchemeIdUriToSystemId_InvalidSchemeId_ReturnsNull()
        {
            const string schemeId = "___invalid___";

            var received = CencUtils.SchemeIdUriToSystemId(schemeId);

            Assert.That(received, Is.Null);
        }

        [Test]
        public void SupportsSchemeIdUri_PlayReadySchemeIdUri_ReturnsTrue()
        {
            const string schemeIdUri = "urn:uuid:9a04f079-9840-4286-ab92-e65be0885f95";

            var received = CencUtils.SupportsSchemeIdUri(schemeIdUri);

            Assert.That(received, Is.True);
        }

        [Test]
        public void SupportsSchemeIdUri_InvalidSchemeIdUri_ReturnsFalse()
        {
            const string schemeIdUri = "___invalid___";

            var received = CencUtils.SupportsSchemeIdUri(schemeIdUri);

            Assert.That(received, Is.False);
        }

        [Test]
        public void SupportsSystemId_PlayReadySystemId_ReturnsTrue()
        {
            var systemId = new byte[]
            {
                0x9a, 0x04, 0xf0, 0x79, 0x98, 0x40, 0x42, 0x86,
                0xab, 0x92, 0xe6, 0x5b, 0xe0, 0x88, 0x5f, 0x95,
            };

            var received = CencUtils.SupportsSystemId(systemId);

            Assert.That(received, Is.True);
        }

        [Test]
        public void SupportsSystemId_InvalidSystemId_ReturnsFalse()
        {
            var systemId = new byte[] { };

            var received = CencUtils.SupportsSystemId(systemId);

            Assert.That(received, Is.False);
        }

        [TestCase("playready")]
        [TestCase("widevine")]
        public void SupportsType_ValidType_ReturnsTrue(string drmType)
        {
            var received = CencUtils.SupportsType(drmType);

            Assert.That(received, Is.True);
        }

        [Test]
        public void SupportsType_InvalidType_ReturnsFalse()
        {
            const string drmType = "___invalid___";

            var received = CencUtils.SupportsType(drmType);

            Assert.That(received, Is.False);
        }

        [Test]
        public void GetKeySystemName_PlayReadySystemId_ReturnsValid()
        {
            var systemId = new byte[]
            {
                0x9a, 0x04, 0xf0, 0x79, 0x98, 0x40, 0x42, 0x86,
                0xab, 0x92, 0xe6, 0x5b, 0xe0, 0x88, 0x5f, 0x95,
            };

            var received = CencUtils.GetKeySystemName(systemId);

            Assert.That(received, Is.EqualTo("com.microsoft.playready"));
        }

        [Test]
        public void GetKeySystemName_InvalidSystemId_ReturnsNull()
        {
            var systemId = new byte[] { };

            var received = CencUtils.GetKeySystemName(systemId);

            Assert.That(received, Is.Null);
        }

        [Test]
        public void GetScheme_PlayReadySystemId_ReturnsValid()
        {
            var systemId = new byte[]
            {
                0x9a, 0x04, 0xf0, 0x79, 0x98, 0x40, 0x42, 0x86,
                0xab, 0x92, 0xe6, 0x5b, 0xe0, 0x88, 0x5f, 0x95,
            };

            var received = CencUtils.GetScheme(systemId);

            Assert.That(received, Is.EqualTo("playready"));
        }

        [Test]
        public void GetScheme_InvalidSystemId_ReturnsUnknown()
        {
            var systemId = new byte[] { };

            var received = CencUtils.GetScheme(systemId);

            Assert.That(received, Is.EqualTo("unknown"));
        }

        [TestCase("playready", CencUtils.DrmType.PlayReady)]
        [TestCase("widevine", CencUtils.DrmType.Widevine)]
        public void GetDrmType_ValidType_ReturnsValid(string type, CencUtils.DrmType expected)
        {
            var received = CencUtils.GetDrmType(type);

            Assert.That(received, Is.EqualTo(expected));
        }

        [Test]
        public void GetDrmType_InvalidType_ReturnsUnknown()
        {
            const string type = "___invalid___";

            var received = CencUtils.GetDrmType(type);

            Assert.That(received, Is.EqualTo(CencUtils.DrmType.Unknown));
        }
    }
}