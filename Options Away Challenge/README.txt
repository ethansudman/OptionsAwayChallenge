Options Away Coding Challenge
=============================

The Options Away Business Intelligence team has submitted a request for an ETL job and you are tasked with writing a program that will be used as part of the ETL task. The BI team needs to take historical flight purchase data and separate it into some pre-defined buckets that will make it easier for them to generate their daily reports. For this challenge you have been supplied with two files: 

	* flight_data.csv: A comma-separated sampling of historical flight option purchase data. The fields in this file are as follows:
		*order_id: The Options Away order number (eg. 7639)
		*flight_number: The number of the flight (eg. UA-172)
		*airline: The airline the flight is on (eg. United)
		*origin: The airport code where the flight will depart from (eg. ORD)
		*destination: The airport code where the flight will arrive (eg. SFO)
		*cabin_class: An integer between 1 and 7 indicating the type of ticket purchased (eg. 1)
		*option_duration: An string indicating how many days the user locked in a fare for (eg. 2_day)
		*flight_departure_datetime: A datetime stamp indicating the departure date and time of the flight (eg. 2015-06-30 12:25:12)

	* flight_buckets.csv: A comma-separated list of bucket definitions provided by the BI team. The fields in this file are as follows:
		*airline
		*cabin_class
		*option_duration

You must write a program which categorizes each flight listed in flight_data.csv into the most specific bucket found in the flight_buckets.csv file. It is important to note that due to flight purhcase information coming from a variety of sources the casing of the strings contained in the file is not guaranteed to be consistent. Therefore all string comparisons should be done in a case-insensitive fashion. The rules for comparing the relative specificity of two buckets are as follows:

*Any actual value (eg. Delta) is more specific than a wildcard character
*In the case of a specificity tie, airline is most specific, option_duration is second most specific and cabin_class is least specific. So for instance if you had a flight that had the attributes { "airline": "Delta", "cabin_class": "1", "option_duration": "2_day" } and 2 valid buckets with equal specficity for the flight like { "airline": "Delta", "cabin_class": "1", "option_duration": "*" } and { "airline": "*", "cabin_class": "1", "option_duration": "2_day"} then the flight should be put in the first bucket (where "Delta" is specified) because airline is more specific than cabin_class or option_duration.

Flight buckets are defined as combinations of attributes a flight posesses. For instance, here are some example flights like you might find in the flight_data.csv file:

7639,UA-172,United,ORD,SFO,2,1_day,2015-06-30 12:25:00
7640,UA-1420,United,ORD,ATL,1,3_day,2015-07-31 10:31:00
7641,D-283,Delta,DFW,LAX,3,5_day,2015-10-31 17:35:00
7642,SW-2938,Southwest,DEN,ORD,2,1_day,2015-05-14 11:35:00
7643,UA-172,United,ORD,SFO,1,1_day,2015-06-30 12:25:00
7644,VA-9930,Virgin,DFW,DEN,3,5_day,2015-5-15 14:25:00

And here is some example bucket data ('*' is a wildcard and indicates that any value is valid for that field):
united,*,* (all United flights)
united,2,* (all United flights with a cabin_class equal to 2)
united,1,3_day (all United flights with a cabin_class equal to 1 where a 3_day hold was purchased)
southwest,*,* (all Southwest flights regardless of cabin_class or option_duration)

The BI team wants each purchase (identified by order_id) sorted into the most specific bucket possible and only into that bucket. Flights that don't fit into any bucket should be put into an 'uncategorized (*,*,*)' bucket. The program's output should just be a JSON file that lists each bucket with the flight data for flights that are in it. So for the four buckets and six flights listed above the output of the program should be:

[    
    {
    	"bucket": "*,*,*",
    	"flights": [
			"7641,D-283,Delta,DFW,LAX,3,5_day,2015-10-31 17:35:00",
			"7644,VA-9930,Virgin,DFW,DEN,3,5_day,2015-5-15 14:25:00"
	    ]
	},
    {
    	"bucket": "southwest,*,*",
	    "flights": [
			"7642,SW-2938,Southwest,DEN,ORD,2,1_day,2015-05-14 11:35:00"
	    ]
    },
	{
	    "bucket": "united,*,*",
	    "flights": [
			"7643,UA-172,United,ORD,SFO,1,1_day,2015-06-30 12:25:00"
	    ]
    },
    {
	    "bucket": "united,1,3_day",
	    "flights": [
			"7640,UA-1420,United,ORD,ATL,1,3_day,2015-07-31 10:31:00"
    	]
    },
    {
	    "bucket": "united,2,*",
	    "flights": [
			"7639,UA-172,United,ORD,SFO,2,1_day,2015-06-30 12:25:00"
	    ]
    }
]

While a Python solution is preferred, if you have a good reason to want to use another language you are free to. Just be prepared to defend your decision.

Take this as an opportunity to show what you can do. We are interested in seeing how you would approach this problem in a professional role, so the shortest answer might not be the best. You should also consider standard things like code quality, maintainability, etc.

Please submit the source files that make up your solution, the JSON-formatted answer set and any readme's or any other supporting documentation that you feel will make your solution easier for us to understand.

To make your answer easy to check for correctness, please order your JSON output first by bucket and then within each bucket order the purchases by order_id. If you have any questions or encounter any issues with the problem or the datasets please contact Steve Kain at Options Away directly via email at skain@optionsaway.com.

Thanks for your interest and good luck!