using FaceClientSDK.Domain.LargeFaceList;
using FaceClientSDK.Tests.Fixtures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Xunit;

namespace FaceClientSDK.Tests
{
    public class LargeFaceListTests : IClassFixture<FaceAPISettingsFixture>
    {
        private FaceAPISettingsFixture faceAPISettingsFixture = null;

        public LargeFaceListTests(FaceAPISettingsFixture fixture)
        {
            faceAPISettingsFixture = fixture;

            APIReference.FaceAPIKey = faceAPISettingsFixture.FaceAPIKey;
            APIReference.FaceAPIZone = faceAPISettingsFixture.FaceAPIZone;
        }

        [Fact]
        public async void AddFaceAsyncTest()
        {
            AddFaceResult result = null;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                var creation_result = await APIReference.Instance.LargeFaceList.CreateAsync(identifier, identifier, identifier);

                if (creation_result)
                {
                    dynamic jUserData = new JObject();
                    jUserData.UserDataSample = "User Data Sample";
                    var rUserData = JsonConvert.SerializeObject(jUserData);

                    result = await APIReference.Instance.LargeFaceList.AddFaceAsync(identifier, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await APIReference.Instance.LargeFaceList.DeleteAsync(identifier);
            }

            Assert.True(result != null);
        }

        [Fact]
        public async void CreateAsyncTest()
        {
            bool result = false;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                result = await APIReference.Instance.LargeFaceList.CreateAsync(identifier, identifier, identifier);
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await APIReference.Instance.LargeFaceList.DeleteAsync(identifier);
            }

            Assert.True(result);
        }

        [Fact]
        public async void DeleteAsyncTest()
        {
            bool result = false;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                var creation_result = await APIReference.Instance.LargeFaceList.CreateAsync(identifier, identifier, identifier);

                result = await APIReference.Instance.LargeFaceList.DeleteAsync(identifier);
            }
            catch
            {
                throw;
            }

            Assert.True(result);
        }

        [Fact]
        public async void DeleteFaceAsyncTest()
        {
            bool result = false;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                var creation_result = await APIReference.Instance.LargeFaceList.CreateAsync(identifier, identifier, identifier);

                AddFaceResult addface_result = null;
                if (creation_result)
                {
                    dynamic jUserData = new JObject();
                    jUserData.UserDataSample = "User Data Sample";
                    var rUserData = JsonConvert.SerializeObject(jUserData);

                    addface_result = await APIReference.Instance.LargeFaceList.AddFaceAsync(identifier, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty);

                    if (addface_result != null)
                        result = await APIReference.Instance.LargeFaceList.DeleteFaceAsync(identifier, addface_result.persistedFaceId);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await APIReference.Instance.LargeFaceList.DeleteAsync(identifier);
            }

            Assert.True(result);
        }

        [Fact]
        public async void GetAsyncTest()
        {
            GetResult result = null;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                var creation_result = await APIReference.Instance.LargeFaceList.CreateAsync(identifier, identifier, identifier);

                if (creation_result)
                    result = await APIReference.Instance.LargeFaceList.GetAsync(identifier);
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await APIReference.Instance.LargeFaceList.DeleteAsync(identifier);
            }

            Assert.True(result != null);
        }

        [Fact]
        public async void GetFaceAsyncTest()
        {
            GetFaceResult result = null;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                var creation_result = await APIReference.Instance.LargeFaceList.CreateAsync(identifier, identifier, identifier);

                AddFaceResult addface_result = null;
                if (creation_result)
                {
                    dynamic jUserData = new JObject();
                    jUserData.UserDataSample = "User Data Sample";
                    var rUserData = JsonConvert.SerializeObject(jUserData);

                    addface_result = await APIReference.Instance.LargeFaceList.AddFaceAsync(identifier, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty);

                    if (addface_result != null)
                        result = await APIReference.Instance.LargeFaceList.GetFaceAsync(identifier, addface_result.persistedFaceId);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await APIReference.Instance.LargeFaceList.DeleteAsync(identifier);
            }

            Assert.True(result != null);
        }

        [Fact]
        public async void GetTrainingStatusAsyncTest()
        {
            GetTrainingStatusResult result = null;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                var creation_result = await APIReference.Instance.LargeFaceList.CreateAsync(identifier, identifier, identifier);

                AddFaceResult addface_result = null;
                if (creation_result)
                {
                    dynamic jUserData = new JObject();
                    jUserData.UserDataSample = "User Data Sample";
                    var rUserData = JsonConvert.SerializeObject(jUserData);

                    addface_result = await APIReference.Instance.LargeFaceList.AddFaceAsync(identifier, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty);

                    if (addface_result != null)
                    {
                        bool training_result = false;
                        training_result = await APIReference.Instance.LargeFaceList.TrainAsync(identifier);

                        if (training_result)
                        {
                            while (true)
                            {
                                System.Threading.Tasks.Task.Delay(1000).Wait();
                                result = await APIReference.Instance.LargeFaceList.GetTrainingStatusAsync(identifier);

                                if (result.status == "running")
                                {
                                    continue;
                                }
                                else if (result.status == "succeeded")
                                {
                                    break;
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }                            
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await APIReference.Instance.LargeFaceList.DeleteAsync(identifier);
            }

            Assert.True(result != null);
        }

        [Fact]
        public async void ListAsyncTest()
        {
            List<ListResult> result = null;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                var creation_result = await APIReference.Instance.LargeFaceList.CreateAsync(identifier, identifier, identifier);

                if (creation_result)
                    result = await APIReference.Instance.LargeFaceList.ListAsync(string.Empty, 1000);
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await APIReference.Instance.LargeFaceList.DeleteAsync(identifier);
            }

            Assert.True(result != null);
        }

        [Fact]
        public async void ListFaceAsyncTest()
        {
            List<ListFaceResult> result = null;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                var creation_result = await APIReference.Instance.LargeFaceList.CreateAsync(identifier, identifier, identifier);

                AddFaceResult addface_result = null;
                if (creation_result)
                {
                    dynamic jUserData = new JObject();
                    jUserData.UserDataSample = "User Data Sample";
                    var rUserData = JsonConvert.SerializeObject(jUserData);

                    addface_result = await APIReference.Instance.LargeFaceList.AddFaceAsync(identifier, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty);

                    if (addface_result != null)
                        result = await APIReference.Instance.LargeFaceList.ListFaceAsync(identifier);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await APIReference.Instance.LargeFaceList.DeleteAsync(identifier);
            }

            Assert.True(result != null);
        }

        [Fact]
        public async void TrainAsyncTest()
        {
            bool result = false;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                var creation_result = await APIReference.Instance.LargeFaceList.CreateAsync(identifier, identifier, identifier);

                AddFaceResult addface_result = null;
                if (creation_result)
                {
                    dynamic jUserData = new JObject();
                    jUserData.UserDataSample = "User Data Sample";
                    var rUserData = JsonConvert.SerializeObject(jUserData);

                    addface_result = await APIReference.Instance.LargeFaceList.AddFaceAsync(identifier, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty);

                    if (addface_result != null)
                    {
                        result = await APIReference.Instance.LargeFaceList.TrainAsync(identifier);

                        while (true)
                        {
                            System.Threading.Tasks.Task.Delay(1000).Wait();
                            var status = await APIReference.Instance.LargeFaceList.GetTrainingStatusAsync(identifier);

                            if (status.status == "running")
                            {
                                continue;
                            }
                            else if (status.status == "succeeded")
                            {
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await APIReference.Instance.LargeFaceList.DeleteAsync(identifier);
            }

            Assert.True(result);
        }

        [Fact]
        public async void UpdateAsyncTest()
        {
            bool result = false;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                var creation_result = await APIReference.Instance.LargeFaceList.CreateAsync(identifier, identifier, identifier);
                System.Diagnostics.Trace.Write($"Creation Result: {creation_result}");

                if (creation_result)
                    result = await APIReference.Instance.LargeFaceList.UpdateAsync(identifier, "Name", "User Data Sample");
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await APIReference.Instance.LargeFaceList.DeleteAsync(identifier);
            }

            Assert.True(result);
        }

        [Fact]
        public async void UpdateFaceAsyncTest()
        {
            bool result = false;
            var identifier = System.Guid.NewGuid().ToString();

            try
            {
                var creation_result = await APIReference.Instance.LargeFaceList.CreateAsync(identifier, identifier, identifier);

                AddFaceResult addface_result = null;
                if (creation_result)
                {
                    dynamic jUserData = new JObject();
                    jUserData.UserDataSample = "User Data Sample";
                    var rUserData = JsonConvert.SerializeObject(jUserData);

                    addface_result = await APIReference.Instance.LargeFaceList.AddFaceAsync(identifier, faceAPISettingsFixture.TestImageUrl, rUserData, string.Empty);

                    if (addface_result != null)
                        result = await APIReference.Instance.LargeFaceList.UpdateFaceAsync(identifier, addface_result.persistedFaceId, "User Data Sample");
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                var deletion_result = await APIReference.Instance.LargeFaceList.DeleteAsync(identifier);
            }

            Assert.True(result);
        }
    }
}