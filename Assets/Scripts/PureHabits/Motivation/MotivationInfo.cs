namespace PureHabits.Motivation
{
public class MotivationInfo
{
    public string Author { get; set; }
    public string Content { get; set; }

    private MotivationInfo(string author, string content)
    {
        Author = author;
        Content = content;
    }

    // Статическое поле для хранения всех мотивационных речей
    public static readonly MotivationInfo[] Motivations = new MotivationInfo[]
    {
        new MotivationInfo("Nelson Mandela", "It always seems impossible until it’s done."),
        new MotivationInfo("Albert Einstein", "Life is like riding a bicycle. To keep your balance you must keep moving."),
        new MotivationInfo("Walt Disney", "The way to get started is to quit talking and begin doing."),
        new MotivationInfo("Confucius", "It does not matter how slowly you go as long as you do not stop."),
        new MotivationInfo("Tony Robbins", "The only limit to your impact is your imagination and commitment."),
        new MotivationInfo("Oprah Winfrey", "The biggest adventure you can take is to live the life of your dreams."),
        new MotivationInfo("Mahatma Gandhi", "You must be the change you wish to see in the world."),
        new MotivationInfo("Henry Ford", "Whether you think you can, or you think you can't – you're right."),
        new MotivationInfo("Babe Ruth", "It’s hard to beat a person who never gives up."),
        new MotivationInfo("Steve Jobs", "Innovation distinguishes between a leader and a follower."),
        new MotivationInfo("Elon Musk", "When something is important enough, you do it even if the odds are not in your favor."),
        new MotivationInfo("Eleanor Roosevelt", "The future belongs to those who believe in the beauty of their dreams."),
        new MotivationInfo("Zig Ziglar", "You don’t have to be great to start, but you have to start to be great."),
        new MotivationInfo("Les Brown", "Shoot for the moon. Even if you miss it you will land among the stars."),
        new MotivationInfo("J.K. Rowling", "It is our choices that show what we truly are, far more than our abilities."),
        new MotivationInfo("Mark Twain", "The secret of getting ahead is getting started."),
        new MotivationInfo("Winston Churchill", "Success is not final, failure is not fatal: It is the courage to continue that counts."),
        new MotivationInfo("Abraham Lincoln", "The best way to predict the future is to create it."),
        new MotivationInfo("Albert Einstein", "Strive not to be a success, but rather to be of value."),
        new MotivationInfo("Vince Lombardi", "Perfection is not attainable, but if we chase perfection we can catch excellence."),
        new MotivationInfo("Charles Kettering", "High achievement always takes place in the framework of high expectation."),
        new MotivationInfo("Henry David Thoreau", "Go confidently in the direction of your dreams. Live the life you have imagined."),
        new MotivationInfo("Pablo Picasso", "Action is the foundational key to all success."),
        new MotivationInfo("C.S. Lewis", "You are never too old to set another goal or to dream a new dream."),
        new MotivationInfo("Ralph Waldo Emerson", "What lies behind us and what lies before us are tiny matters compared to what lies within us."),
        new MotivationInfo("Denzel Washington", "You pray for rain, you gotta deal with the mud too. That’s a part of it."),
        new MotivationInfo("Wayne Gretzky", "You miss 100% of the shots you don’t take."),
        new MotivationInfo("Michael Jordan", "I can accept failure, everyone fails at something. But I can’t accept not trying."),
        new MotivationInfo("Tim Notke", "Hard work beats talent when talent doesn’t work hard."),
        new MotivationInfo("Maya Angelou", "We may encounter many defeats, but we must not be defeated."),
        new MotivationInfo("Napoleon Hill", "Whatever the mind of man can conceive and believe, it can achieve."),
        new MotivationInfo("Jim Rohn", "Either you run the day or the day runs you."),
        new MotivationInfo("Albert Schweitzer", "The purpose of human life is to serve and to show compassion and the will to help others."),
        new MotivationInfo("J.R.R. Tolkien", "Not all those who wander are lost."),
        new MotivationInfo("Franklin D. Roosevelt", "The only thing we have to fear is fear itself."),
        new MotivationInfo("Martin Luther King Jr.", "Faith is taking the first step even when you don’t see the whole staircase."),
        new MotivationInfo("John F. Kennedy", "Ask not what your country can do for you – ask what you can do for your country."),
        new MotivationInfo("Mahatma Gandhi", "Live as if you were to die tomorrow. Learn as if you were to live forever."),
        new MotivationInfo("Walt Disney", "The difference between winning and losing is most often not quitting."),
        new MotivationInfo("Steve Jobs", "Your time is limited, so don’t waste it living someone else’s life."),
        new MotivationInfo("Confucius", "Our greatest glory is not in never falling, but in rising every time we fall."),
        new MotivationInfo("Oscar Wilde", "Be yourself; everyone else is already taken."),
        new MotivationInfo("Yoda", "Do, or do not. There is no try."),
        new MotivationInfo("Vince Lombardi", "The only place success comes before work is in the dictionary."),
        new MotivationInfo("Marilyn Monroe", "Give a girl the right shoes, and she can conquer the world."),
        new MotivationInfo("Mark Twain", "Kindness is the language which the deaf can hear and the blind can see."),
        new MotivationInfo("Charles Dickens", "No one is useless in this world who lightens the burdens of another."),
        new MotivationInfo("Dale Carnegie", "The only way to influence people is to talk in terms of what the other person wants."),
        new MotivationInfo("Ralph Waldo Emerson", "To be yourself in a world that is constantly trying to make you something else is the greatest accomplishment."),
        new MotivationInfo("Eleanor Roosevelt", "No one can make you feel inferior without your consent."),
        new MotivationInfo("Henry Ford", "If you think you can do a thing or think you can’t do a thing, you’re right."),
        new MotivationInfo("Abraham Lincoln", "The best way to predict the future is to create it."),
        new MotivationInfo("Helen Keller", "Alone we can do so little; together we can do so much."),
        new MotivationInfo("Steve Jobs", "The people who are crazy enough to think they can change the world are the ones who do."),
        new MotivationInfo("Brene Brown", "Courage starts with showing up and letting ourselves be seen."),
        new MotivationInfo("John Wooden", "Success is never final, failure is never fatal. It’s courage that counts."),
        new MotivationInfo("Albert Einstein", "Imagination is more important than knowledge."),
        new MotivationInfo("Winston Churchill", "Success is not final, failure is not fatal: It is the courage to continue that counts."),
        new MotivationInfo("Jim Rohn", "The major key to your better future is you."),
        new MotivationInfo("Nelson Mandela", "Do not judge me by my successes, judge me by how many times I fell down and got back up again."),
        new MotivationInfo("Dale Carnegie", "You can make more friends in two months by becoming interested in other people than you can in two years by trying to get other people interested in you."),
        new MotivationInfo("Maya Angelou", "You may not control all the events that happen to you, but you can decide not to be reduced by them.")
    };
}

}