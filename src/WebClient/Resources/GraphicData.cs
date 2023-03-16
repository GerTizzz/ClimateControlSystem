﻿namespace WebClient.Resources
{
    public record GraphicData(string time, float value, string type)
    {
        public override string ToString()
        {
            return $"X: {time} Y: {value} Type: {type}";
        }
    }
}