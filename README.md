Yahoo Finance Data

How to install:
Click on the github link below and you will be taken to the repository, click on the “clone or download” button at the end and choose DownloadZip.
https://github.com/dash2ravi/MarketData
Once you have the zip file, extract it.

How to run:
If you want to run the application directly, the application file is present in
.\ MarketData-master\MarketData-master\YahooFinanceData\bin\Debug\YahooFinanceData.exe
The output is downloaded to 
.\ MarketData-master\MarketData-master\YahooFinanceData\Entities
Under the name “ymarketdataCSV.csv” or “ymarketdataJSON.txt” based on the chosen option.

Imp: To leave the app running every 1 hour, add “YahooFinanceData.exe” to Windows Scheduled Tasks. The application has the functionality to run it with or without user entering the output format. It gives user 10 seconds to choose an option upon start and if nothing is entered, the default value from the “app.config” file is taken.
Addition info about Market list and settings.
Based on the type of fields requested, which is not publicly available to download from Yahoo finance as  a parameter set to the url, I assumed to store additional info about the market in a local file (Ideally I would have stored it in a proper database, but due to the scope of the project I am getting that info from a text file. It is very easy to change that request to the database.)
If the user needs to request for more markets, that can be easily added to “MarketList.txt” file present within the same project under “Entities” folder. As you are requesting info periodically, it is better to have a comprehensive list of tickers with additional info on their exchanges and session times etc. That can be added to “ContractInfo.txt” present in “Entities” folder.
The program can also be run by opening the solutions file from the extracted folder in Visual Studio and pressing start. 



Classes:
MarketDataHandler // implements IMarketData interface which has methods
ParseResponse: This method as the name says takes the response from WebClient Download string and divides them into columns
and 

FormarOutput: This function formats the output based on the user setting into JSon or csv.
MarketDataHandler also has an additional method “LoadContracts“ which allows it to get info from the ContractInfo.txt file
RequestData // This class is responsible for building the request as per the user’s need
Contract // Has all properties for the contract
Option // Has a property which stores the user’s requested format 
Program // Contains Main function where the program starts

Testing:
I have tested the application by entering false symbols and the application handles them well.
Also, tested running the application with the output file already open elsewhere, user is notified about that too. 
Any other exception is caught gracefully using the “System.Exception” class and the application runs uninterrupted.

