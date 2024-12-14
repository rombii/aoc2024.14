using aoc2024._14;

using var inputReader = new StreamReader(Path.Join(Directory.GetCurrentDirectory(), "input.txt"));
const int limitX = 101;
const int limitY = 103;

var robots = new List<Robot>(); 
while (!inputReader.EndOfStream)
{
    var line = await inputReader.ReadLineAsync();
    var values = line!.Split(' ');
    var start = values[0].Split('=')[1].Split(',').Select(int.Parse).ToArray();
    var move = values[1].Split('=')[1].Split(',').Select(int.Parse).ToArray();
    var newRobot = new Robot(start[0], start[1], move[0], move[1]);
    robots.Add(newRobot);
}

var sec = 0;
int[,] map;
while (true) 
{
    sec++;
    Console.Clear();
    var printing = false;
    map = new int[limitY, limitX];
    foreach (var robot in robots)
    {
        robot.Update(limitX, limitY);
        map[robot.Y, robot.X] = 1;
        printing = FiveInLineCheck(robot.X, robot.Y);
    } 
// uncomment to get part1 answer
//    if (sec == 100)
//    {
//        Console.WriteLine(GetPart1Answer());
//        Console.ReadKey();
//    }

    if (printing)
    {
        Console.WriteLine(sec);
        for (var i = 0; i < limitY; i++)
        {
            for (var j = 0; j < limitX; j++)
            {
                Console.Write((map[i, j] == 1 ? '#' : '.') + " ");
            }

            Console.WriteLine();
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}

bool FiveInLineCheck(int y, int x)
{
    if (x - 2 < 0 || x + 2 >= limitX) return false;
    return map[x - 2, y] == 1 && map[x - 1, y] == 1 && map[x, y] == 1 && map[x + 1, y] == 1 && map[x + 2, y] == 1;
}
int GetQuadrantNumber(int x, int y)
{
    return (x, y) switch
    {
        (< limitX / 2, < limitY / 2) => 0,
        (> limitX / 2, < limitY / 2) => 1,
        (< limitX / 2, > limitY / 2) => 2,
        (> limitX / 2, > limitY / 2) => 3,
        _ => -1,
    };
}

long GetPart1Answer()
{
    var quadrants = new Dictionary<int, List<(int, int)>>
    {
        {0, []},
        {1, []},
        {2, []},
        {3, []},
    };

    foreach (var robot in robots)
    {
        var quadrant = GetQuadrantNumber(robot.X, robot.Y);
        if (quadrants.TryGetValue(quadrant, out var value))
        {
            value.Add((robot.X, robot.Y));
        }
    }

    return quadrants.Aggregate<KeyValuePair<int, List<(int, int)>>, long>(1, (current, quadrant) => current * quadrant.Value.Count);
}