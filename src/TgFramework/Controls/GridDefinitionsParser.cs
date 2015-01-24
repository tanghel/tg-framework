using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TgFramework.Core;
using TgFramework.Exceptions;

namespace TgFramework.Controls
{
    public static class GridDefinitionExtensions
    {
        private static GridLength GetGridLength(string definition)
        {
            if (definition == null)
            {
                throw new ArgumentNullException("definition");
            }

            if (definition.Length == 0)
            {
                throw new ArgumentException("definition length should be greater than zero!");
            }

            if (definition.Equals("*"))
            {
                return new GridLength(1, GridUnitType.Star);
            }

            if (definition.Equals("Auto"))
            {
                return GridLength.Auto;
            }

            var unitType = GridUnitType.Pixel;
            var valueDefinition = definition;
            if (definition.EndsWith("*"))
            {
                unitType = GridUnitType.Star;
                valueDefinition = definition.TrimEnd('*');
            }

            var length = valueDefinition.ToNullableDouble();
            if (length == null)
            {
                throw new GridDefinitionParseException(definition, "Could not extract value");
            }

            return new GridLength((double)length, unitType);
        }

        private static Tuple<int, GridLength> GetGridLengths(string definition)
        {
            if (definition == null)
            {
                throw new ArgumentNullException("definition");
            }

            if (definition.EndsWith("x"))
            {
                throw new GridDefinitionParseException(definition, "Should not end in multiplier character");
            }

            if (definition.Contains('x'))
            {
                var index = definition.IndexOf('x');

                var left = definition.Substring(0, index);
                var right = definition.Substring(index + 1);

                var multiplier = left.ToNullableInt();
                if (multiplier == null)
                {
                    throw new GridDefinitionParseException(definition, "Could not extract multiplier");
                }

                return new Tuple<int,GridLength>((int)multiplier, GetGridLength(right));
            }

            return new Tuple<int, GridLength>(1, GetGridLength(definition));
        }

        private static IEnumerable<GridLength> GetAllGridLengths(string lengths)
        {
            if (lengths == null)
            {
                throw new ArgumentNullException("lengths");
            }

            var definitions = lengths.Split(',').Select(x => x.Trim());
            foreach (var definition in definitions)
            {
                var multiplier = GetGridLengths(definition);
                for (int i = 0; i < multiplier.Item1; i++)
                {
                    yield return multiplier.Item2;
                }
            }
        }

        public static void SetRowDefinitions(this Grid grid, string rows)
        {
            if (grid == null)
            {
                throw new ArgumentNullException("grid");
            }

            if (rows == null)
            {
                throw new ArgumentNullException("rows");
            }

            grid.RowDefinitions.Clear();
            foreach (var gridLength in GetAllGridLengths(rows))
            {
                grid.RowDefinitions.Add(new RowDefinition() { Height = gridLength });
            }
        }

        public static void SetColumnDefinitions(this Grid grid, string columns)
        {
            if (grid == null)
            {
                throw new ArgumentNullException("grid");
            }

            if (columns == null)
            {
                throw new ArgumentNullException("columns");
            }

            grid.ColumnDefinitions.Clear();
            foreach (var gridLength in GetAllGridLengths(columns))
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = gridLength });
            }
        }
    }
}
