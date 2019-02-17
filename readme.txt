
I have enumerated some comments about this exercice:

1.- The App-keys for the custom search will be actived for 15 days more. If you need to test after that period please update with a new generated keys in the config file.
2.- I have generated a custom config section called ConfigSearchSection. 
3.- The application can accept new searches libraries dinamically. You only need to implement the interface Isearch, add the ConfigSearchSection  and copy the dll in the same folder
4.- I have used some design patterns like sigleton and Abstract factory pattern.
5.- In order to create a dinamic solution I used reflection  libraries.

