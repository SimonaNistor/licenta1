# -*- coding: utf-8 -*-
"""
Created on Tue Mar 13 14:36:58 2018

@author: Simona
"""
import clr
import System
import urllib
clr.AddReference("System.Core")
#clr.AddReference("requests")

page = urllib.get("https://www.tutorialspoint.com/cplusplus/cpp_constructor_destructor.htm")
page.status_code

from beautifulsoup4 import BeautifulSoup
soup = BeautifulSoup(page.content, 'html.parser')


#fetch page title which will be used to associate the snippets to a keyword
somethingnew = soup.select('h1')[1]
#print (somethingnew)

#fetch all code snippets and save the first one to a txt file

something = soup.find_all(class_="prettyprint notranslate")
if not something:
    something = soup.find_all(class_="prettyprint notranslate prettyprinted")
    
#ceva = something[2]

numar = len(something)
#print(something[1])

#content=''.join(something[0])

#with open('myfile.txt', 'w') as f:
#    f.write(content)