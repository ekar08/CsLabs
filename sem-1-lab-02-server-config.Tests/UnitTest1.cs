using System.Runtime.CompilerServices;
using NUnit.Framework;
using sem_1_lab_02_server_config;

namespace sem_1_lab_02_server_config.Tests;

public class Tests
{
    [Test]
    static void Team_mode_check()
    {
        var result = Program.Team_mode();
        Assert.That(result, Is.EqualTo());
    }
}
