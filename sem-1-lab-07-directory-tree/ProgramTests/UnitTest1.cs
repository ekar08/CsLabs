using lab07;
namespace ProgramTests;

public class Tests
{
    [Test]
    public void TestGetIndent()
    {
        
        lab07.Program._depth = 1;
        string getIndent = lab07.Program.GetIndent();
        
        string my = "│ ";
        Assert.That(getIndent, Is.EqualTo(my));
    }

    
}
