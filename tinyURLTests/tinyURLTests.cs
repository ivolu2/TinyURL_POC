using TinyURL_POC;

namespace tinyURLTests;

[TestClass]
public class tinyURLTests
{
    [TestMethod]
    public void TestCreateTinyURL()
    {
        //Arrange
        DefaultTokenGenerator tokenGenerator = new();
        TinyURLService tinyURLService = new(tokenGenerator);

        //Act
        string longURL = "https://www.google.com";
        string tinyURL = tinyURLService.createTinyURL(longURL);
        string token = tinyURL.Substring(20);

        //Assert
        Assert.IsNotNull(tinyURL); //check if tinyURL is null
        Assert.IsTrue(Uri.IsWellFormedUriString(tinyURL, UriKind.Absolute)); //check if tinyURL is of valid format for Uri
        Assert.IsTrue(tinyURLService.tinyURLs.ContainsKey(tinyURL)); //check if tinyURL mapping was added
        Assert.IsTrue(tinyURLService.tinyURLs[tinyURL].longUrl == longURL); //check if tinyURL to longURL mapping is correct
        Assert.IsNotNull(token); //check if token is null
        Assert.IsTrue(tinyURLService.tokens.Contains(token)); //check if token was added 

    }

    [TestMethod]
    public void TestDeleteTinyURL()
    {

        //Arrange
        DefaultTokenGenerator tokenGenerator = new();
        TinyURLService tinyURLService = new(tokenGenerator);
        

        //Act
        string longURL = "https://www.google.com";
        string tinyURL = tinyURLService.createTinyURL(longURL);
        string token = tinyURL.Substring(20);
        tinyURLService.deleteTinyURL(tinyURL);

        //Assert
        Assert.IsFalse(tinyURLService.tinyURLs.ContainsKey(tinyURL)); //Check if tinyURL mapping was deleted
        Assert.IsFalse(tinyURLService.tokens.Contains(token)); //Check if token was deleted

    }

    [TestMethod]
    public void TestGetLongURL()
    {
        //Arrange
        DefaultTokenGenerator tokenGenerator = new();
        TinyURLService tinyURLService = new(tokenGenerator);

        //Act
        string longURL = "https://www.google.com";
        string tinyURL = tinyURLService.createTinyURL(longURL);
        string getLongURL = tinyURLService.getLongURL(tinyURL);

        //Assert
        Assert.IsNotNull(tinyURL); //check if tinyURL is null
        Assert.IsTrue(Uri.IsWellFormedUriString(tinyURL, UriKind.Absolute)); //check if tinyURL is of valid format for Uri
        Assert.IsTrue(Uri.IsWellFormedUriString(longURL, UriKind.Absolute)); //check if longURL is of valid format for Uri
        Assert.IsNotNull(getLongURL); //check if getLongURL is null
        Assert.IsTrue(Uri.IsWellFormedUriString(getLongURL, UriKind.Absolute)); //check if getLongURL is of valid format for Uri
        Assert.IsTrue(longURL == getLongURL); //check if longURL == getLongURL

    }

    [TestMethod]
    public void TestVisitURL()
    {
        //Arrange
        DefaultTokenGenerator tokenGenerator = new();
        TinyURLService tinyURLService = new(tokenGenerator);

        //Act
        string longURL = "https://www.google.com";
        string tinyURL = tinyURLService.createTinyURL(longURL);
        int count = tinyURLService.visit(tinyURL);

        //Assert
        Assert.IsNotNull(tinyURL); //Check if tinyURL is null
        Assert.IsTrue(count == 1); //Check if visit value was updated

    }

    [TestMethod]
    public void TestGetStats()
    {
        //Arrange
        DefaultTokenGenerator tokenGenerator = new();
        TinyURLService tinyURLService = new(tokenGenerator);

        //Act
        string longURL = "https://www.google.com";
        string tinyURL = tinyURLService.createTinyURL(longURL);
        int count = tinyURLService.visit(tinyURL);
        count = tinyURLService.visit(tinyURL);
        count = tinyURLService.visit(tinyURL); 

        //Assert
        Assert.IsNotNull(tinyURL); //Check if tinyURL is null
        Assert.IsTrue(count == 3); //Check if Visit Count is equal to 3 (3 visit calls)
    }
    
}
