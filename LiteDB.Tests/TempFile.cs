﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LiteDB.Tests
{
    public class TempFile : IDisposable
    {
        public string Filename { get; private set; }

        public TempFile(string ext = "db")
        {
            this.Filename = Path.GetFullPath(string.Format("test-{0}.{1}", Guid.NewGuid(), ext));
        }

        public IDiskService Disk(bool journal = true)
        {
            return new FileDiskService(Filename, journal);
        }

        public void Dispose()
        {
            File.Delete(this.Filename);
        }

        public long Size
        {
            get { return new FileInfo(this.Filename).Length; }
        }

        public string ReadAsText()
        {
            return File.ReadAllText(this.Filename);
        }

        #region LoremIpsum Generator

        public static string LoremIpsum(int minWords, int maxWords,
            int minSentences, int maxSentences,
            int numParagraphs)
        {
            var words = new[] { "lorem", "ipsum", "dolor", "sit", "amet", "consectetuer",
                "adipiscing", "elit", "sed", "diam", "nonummy", "nibh", "euismod",
                "tincidunt", "ut", "laoreet", "dolore", "magna", "aliquam", "erat" };

            var rand = new Random(DateTime.Now.Millisecond);
            var numSentences = rand.Next(maxSentences - minSentences) + minSentences + 1;
            var numWords = rand.Next(maxWords - minWords) + minWords + 1;

            var result = new StringBuilder();

            for (int p = 0; p < numParagraphs; p++)
            {
                for (int s = 0; s < numSentences; s++)
                {
                    for (int w = 0; w < numWords; w++)
                    {
                        if (w > 0) { result.Append(" "); }
                        result.Append(words[rand.Next(words.Length)]);
                    }
                    result.Append(". ");
                }
                result.AppendLine();
            }

            return result.ToString();
        }

        #endregion
    }
}