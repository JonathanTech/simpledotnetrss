# Basic Overview of Classes and Their Functions #

### RSSChannel ###
The Main object for creating an RSS feed. After one of these objects is initialized through a constructor you will be able to output the RSS as a formatted string using `CreateRSS()` Method or the `.ToString()` method.

### RSSItem ###
Represents a RSS Item in a RSS Channel. This is where all your posts/articles/comments go. This basically is the list of items that you want to keep the world updated on. There are some basic guidelines for what should go in here, but basically you can put whatever you want in the description, including html.

### RSSImage ###
Used to add a single Image to your RSS feed. This is usually an image that represents the general content of the RSS feed or the company logo for the owner of the feed. This is for the entire channel so it should be something like a logo that represents the feed or what the feed is about.

### RSSCategory ###
Represents a category that you want your RSSItem or RSSChannel to have. You may include as many category elements as you need to, for different domains, and to have an item cross-referenced in different parts of the same domain.

### RSSExtensionTag ###
Used for implementing custom xml namespaces not supported by this library. i.e. Atom and Itunes. I did not want to limit you to only rss 2.0, but I also did not want to figure those other namespaces yet, so if you do extend it feel free to extend the library and make it easy for everyone else too.

### RSSEnclosure ###
This is applyed to Items that should have files associated to them. I.e. you want to have the aggregator (rss reader) download an associated .mp3 file when it reads the rss feed, this is how you do it. Good for podcasting and the like.

## Other included tools ##

### SimpleRSSLink ###
Generates a link on your page for the rss if your page has implemented the SimpleDotNetRSS interface.
I basically wanted to make an easy link creator for an asp.net web page, but I stopped development on this because it seems almost out of scope with the rest of the library and it needs the most work. It is almost a project by itself.

### ISimpleDotNetRSS ###
Used for the above webcontrol.