using CSharpCrawler.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCrawler
{
    class RobotsTxtParser : IRobotsTxtParser
    {
        public RobotsTxt Parse(string rawData)
        {
            if (string.IsNullOrEmpty(rawData))
            {
                throw new ArgumentException("RobotsTxtParser: Parse: Input data is null or empty");
            }

            var robot = new RobotsTxt();
            string[] lines = PrepareData(rawData);
            bool prevDirectiveIsUserAgent = false;
            bool hasDirectives = false;

            foreach (var line in lines)
            {
                if (line.Contains(':'))
                {
                    string directive = GetDirective(line);

                    if (!hasDirectives && directive == "user-agent" && GetValue(line) == "*")
                    {
                        hasDirectives = true;
                        prevDirectiveIsUserAgent = true;
                        continue;
                    }
                    else if (hasDirectives && directive == "user-agent")
                    {
                        if (prevDirectiveIsUserAgent)
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else if (hasDirectives)
                    {
                        if (prevDirectiveIsUserAgent)
                        {
                            prevDirectiveIsUserAgent = false;
                        }

                        switch (directive)
                        {
                            case "disallow":
                                robot.DisallowedPaths.Add(GetValue(line));
                                break;
                            case "allow":
                                robot.AllowedPaths.Add(GetValue(line));
                                break;
                            case "crawl-delay":
                                double result;

                                if (double.TryParse(GetValue(line), out result))
                                {
                                    robot.CrawlDelay = (long)(result * 1000);
                                }

                                break;
                            case "sitemap":
                                robot.Sitemaps.Add(GetValue(line));
                                break;
                            default:
                                robot.UnknownDirectives.Add(directive);
                                break;
                        }
                    }
                }
                else
                {
                    robot.UnknownLines.Add(line);
                }
            }

            return robot;
        }

        private string[] PrepareData(string rawData)
        {
            string[] lines = rawData
                // Разделяем на строки, игнорируем пустые.
                .Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                // Игнорируем сртоки, содержащие только пробелы.
                .Where(s => !string.IsNullOrWhiteSpace(s))
                // Удаляем пробелы в начале и в конце каждой строки.
                .Select(s => s.Trim())
                // Игнорируем строки, начинающиеся с '#', т.е. содержащие только комментарий.
                .Where(s => !s.StartsWith("#"))
                .ToArray();
            
            // Удаляем все комментарии.
            for (var i = 0; i < lines.Length; ++i)
            {
                if (lines[i].IndexOf('#') > 0)
                {
                    lines[i] = lines[i].Remove(lines[i].IndexOf('#'));
                }
            }

            return lines;
        }

        private string GetDirective(string line)
        {
            var index = line.IndexOf(':');

            return index == -1 ? string.Empty
                               : line.Substring(0, index).TrimEnd().ToLower();
        }

        private string GetValue(string line)
        {
            var index = line.IndexOf(':');

            return index == -1 ? string.Empty
                               : line.Substring(index + 1).TrimStart();
        }
    }
}
