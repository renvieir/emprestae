using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Runtime.Serialization.Json;
using System.Windows.Media.Imaging;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using System.Globalization;
using Microsoft.Phone.Shell;

namespace INdT.Services
{
    public static class Util
    {
        private static readonly string DEBUG_TAG = "INdT.Services.Util";

        private static string _encodeJson(object obj)
        {
            string value = "";
            if (obj is string)
            {
                value = obj as string;
            }
            else if (obj is Dictionary<string, object>)
            {
                value = encodeJson(obj as Dictionary<string, object>);
            }
            else if (obj is object[])
            {
                value = encodeJson(obj as object[]);
            }
            else
            {
                value = string.Format(CultureInfo.InvariantCulture, "{0}", obj);
            }
            return value;
        }

        public static string ucword(string word)
        {
            if (!string.IsNullOrEmpty(word))
            {
                if (word.Length > 1)
                {
                    word = word[0].ToString().ToUpper() + word.Substring(1, word.Length - 1);
                }
                else if (word.Length == 1)
                {
                    word = word.ToUpper();
                }
            }
            return word;
        }

        public static string encodeJson(object obj)
        {
            StringBuilder sb = new StringBuilder("");
            if (obj == null)
            {
                return string.Empty;
            }
            else if (obj is Dictionary<string, object>)
            {
                Dictionary<string, object> dictionary = obj as Dictionary<string, object>;
                sb.Append("{");
                foreach (var key in dictionary.Keys)
                {
                    if (dictionary[key] == null)
                    {
                        continue;
                    }
                    if (sb.Length > 1)
                    {
                        sb.Append(", ");
                    }
                    if (dictionary[key] is string)
                    {
                        sb.Append("\"" + key + "\": \"" + _encodeJson(dictionary[key]) + "\"");
                    }
                    else
                    {
                        sb.Append("\"" + key + "\": " + _encodeJson(dictionary[key]));
                    }
                }
                sb.Append("}");
            }
            else if (obj is object[])
            {
                object[] array = obj as object[];
                sb.Append("[");
                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] == null)
                    {
                        continue;
                    }
                    if (sb.Length > 1)
                    {
                        sb.Append(", ");
                    }
                    if (array[i] is string)
                    {
                        sb.Append("\"" + array[i] + "\"");
                    }
                    else
                    {
                        sb.Append(_encodeJson(array[i]));
                    }
                }
                sb.Append("]");
            }
            return sb.ToString();
        }

        public static string encodeDictionaryToQuery(Dictionary<string, object> dictionary, bool questionMark)
        {
            StringBuilder sb = new StringBuilder();

            if (questionMark)
            {
                sb.Append("?");
            }
            foreach (var key in dictionary.Keys)
            {
                if (dictionary[key] == null)
                {
                    continue;
                }
                if (sb.Length > 1)
                {
                    sb.Append("&");
                }
                sb.Append(key + "=" + HttpUtility.UrlEncode(_encodeJson(dictionary[key])));
            }

            return sb.ToString();
        }

        public static string encodeDictionaryToRESTQuery(Dictionary<string, object> dictionary)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var key in dictionary.Keys)
            {
                if (dictionary[key] != null)
                {
                    sb.Append("/" + HttpUtility.UrlEncode(_encodeJson(dictionary[key])));
                }
                else
                {
                    break;
                }
            }

            return sb.ToString();
        }

        public static void debug(string debugTag, string message)
        {
            Debug.WriteLine(debugTag + " => " + message);
        }

        public static string decodeResponseToString(HttpWebResponse response)
        {
            try
            {
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);
                string responseString = streamReader.ReadToEnd();

                responseStream.Close();
                streamReader.Close();

                return responseString;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static JArray decodeResponseToArray(string response)
        {
            JArray json = null;
            try
            {
                if (!string.IsNullOrEmpty(response))
                {
                    json = JArray.Parse(response);
                }
                return json;
            }
            catch
            {
                return null;
            }
        }

        public static JObject decodeResponseToObject(string response)
        {
            JObject json = null;
            try
            {
                if (!string.IsNullOrEmpty(response))
                {
                    json = JObject.Parse(response);
                }
                return json;
            }
            catch
            {
                return null;
            }
        }

        public static object decodeResponseToJson(HttpWebResponse response)
        {
            string responseString = decodeResponseToString(response);
            if (!string.IsNullOrEmpty(responseString))
            {
                object json = decodeResponseToObject(responseString);
                if (json == null)
                {
                    json = decodeResponseToArray(responseString);
                }
                return json;
            }
            return null;
        }

        public static XDocument decodeResponseToXML(HttpWebResponse response)
        {
            return XDocument.Load(response.GetResponseStream());
        }

        private static void _exploreToken(JToken token, string tab)
        {
            if (token == null)
            {
                return;
            }
            switch (token.Type)
            {
                case JTokenType.Object:
                    var properties = (token as JObject).Properties();
                    foreach (var property in properties)
                    {
                        INdT.Services.Util.debug(DEBUG_TAG, tab + property.Name + " (" + property.Value.Type.ToString() + "):");
                        if (property.Value.Type == JTokenType.Array)
                        {
                            _exploreToken(property.Value, tab);
                        }
                        else
                        {
                            _exploreToken(property.Value, tab + "\t");
                        }
                    }
                    break;
                case JTokenType.Array:
                    var array = (token as JArray);
                    int index = 0;
                    foreach (var element in array)
                    {
                        INdT.Services.Util.debug(DEBUG_TAG, tab + "[" + (index++) + "] =>");
                        _exploreToken(element, tab + "\t\t");
                    }
                    break;
                default:
                    if (token.Type == JTokenType.String)
                    {
                        INdT.Services.Util.debug(DEBUG_TAG, tab + HttpUtility.UrlDecode(HttpUtility.HtmlDecode(token.Value<string>())));
                    }
                    else if (token.Type != JTokenType.Null)
                    {
                        INdT.Services.Util.debug(DEBUG_TAG, tab + token.Value<string>());
                    }
                    break;
            }
        }

        public static void exploreToken(JToken token)
        {
            _exploreToken(token, string.Empty);
        }

        public static T deserializeObject<T>(string serializedObject)
        {
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(serializedObject)))
            {
                try
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                    T deserializedObject = (T)serializer.ReadObject(stream);
                    stream.Close();
                    return deserializedObject;
                }
                catch
                {
                    return (T)(object)null;
                }
            }
        }

        public static T deserializeXML<T>(string xml, string defaultNamespace = null)
        {
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T), defaultNamespace);
                    T deserializedObject = (T)serializer.Deserialize(stream);
                    stream.Close();
                    return deserializedObject;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.InnerException.Message);
                    return (T)(object)null;
                }
            }
        }

        public static string serializeObject<T>(T deserializedObject)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                try
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                    serializer.WriteObject(stream, deserializedObject);
                    stream.Position = 0;
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string serializedObject = reader.ReadToEnd();
                        reader.Close();
                        stream.Close();
                        return serializedObject;
                    }
                }
                catch
                {
                    return string.Empty;
                }
            }
        }

        public static string removerAcentos(string palavra)
        {
            if (string.IsNullOrEmpty(palavra))
            {
                return string.Empty;
            }

            string palavraSemAcento = null;
            string caracterComAcento = "áàãâäéèêëíìîïóòõôöúùûüçÁÀÃÂÄÉÈÊËÍÌÎÏÓÒÕÖÔÚÙÛÜÇñÑ";
            string caracterSemAcento = "aaaaaeeeeiiiiooooouuuucAAAAAEEEEIIIIOOOOOUUUUCnN";

            for (int i = 0; i < palavra.Length; i++)
            {
                if (caracterComAcento.IndexOf(Convert.ToChar(palavra.Substring(i, 1))) >= 0)
                {
                    int car = caracterComAcento.IndexOf(Convert.ToChar(palavra.Substring(i, 1)));
                    palavraSemAcento += caracterSemAcento.Substring(car, 1);
                }
                else
                {
                    palavraSemAcento += palavra.Substring(i, 1);
                }
            }

            return palavraSemAcento;
        }

        public static void saveTile(WriteableBitmap bitmap, string fileName)
        {
            using (var userStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var storageFileStream = new IsolatedStorageFileStream(fileName, FileMode.Create, FileAccess.Write, userStore))
                {
                    bitmap.SaveJpeg(storageFileStream, 173, 173, 0, 100);
                }
            }
        }

        public static void saveFile(string fileName, object content)
        {
            using (var userStore = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (var isoFileStream = new IsolatedStorageFileStream(fileName, FileMode.OpenOrCreate, userStore))
                {
                    //Write the data
                    using (var isoFileWriter = new StreamWriter(isoFileStream))
                    {
                        isoFileWriter.Write(content);
                        isoFileWriter.Flush();
                        isoFileWriter.Close();
                    }
                    isoFileStream.Close();
                }
            }
        }

        public static string readFile(string fileName)
        {
            try
            {
                using (var userStore = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    if (!userStore.FileExists(fileName))
                    {
                        return null;
                    }
                    using (var isoFileStream = new IsolatedStorageFileStream(fileName, FileMode.Open, userStore))
                    {
                        using (var isoFileReader = new StreamReader(isoFileStream))
                        {
                            string content = isoFileReader.ReadToEnd();
                            isoFileReader.Close();
                            return content;
                        }
                    }
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        public static void createTileWithRemoteImage(Uri uri, StandardTileData data)
        {
            Uri backgroundImage = data.BackgroundImage;
            Uri backBackgroundImage = data.BackBackgroundImage;

            if ((data.BackgroundImage != null) && data.BackgroundImage.IsAbsoluteUri && !data.BackgroundImage.AbsoluteUri.Contains("isostore:"))
            {
                data.BackgroundImage = null;
            }
            if ((data.BackBackgroundImage != null) && data.BackBackgroundImage.IsAbsoluteUri && !data.BackBackgroundImage.AbsoluteUri.Contains("isostore:"))
            {
                data.BackBackgroundImage = null;
            }

            ShellTile.Create(uri, data);

            ShellTile tile = ShellTile.ActiveTiles.FirstOrDefault(currentTile => currentTile.NavigationUri == uri);

            if (tile != null)
            {
                if (data.BackgroundImage == null)
                {
                    data.BackgroundImage = backgroundImage;
                }
                if (data.BackBackgroundImage == null)
                {
                    data.BackBackgroundImage = backBackgroundImage;
                }

                tile.Update(data);
            }
        }
    }
}
