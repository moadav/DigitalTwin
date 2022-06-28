# Viktig Info
Prosjektet mangler tilganger til ulike komponenter i Azure og er derfor ikke kjørbar

# Deltagere
- Jonas Braut
- Leo Barzinje
- Trond Kristian Waalen
- Mohammed Ali Davami

# Digital Tvilling
En enkel Digital Tvilling laget gjennom Azure med data fra Oslo Bysykkel og Meteorologisk institutt.


## Filstruktur
 - AzureMaps  
    Inneholder en egen Readme fil med forklaring 
 - DigitalTvilling   
    Her ligger Digital Tvilling funksjonalitene laget for å opprette den digitale tvillingen  
    - BysykkelDatafetcher  
       Funksjonaliteter som er laget for å lage modeller og lagre verdier til Azure SQL databasen  
      - Migrations  
          Inneholder database migrasjoner  
      - utils  
          Har flere klasser for å lage database modeller  
      - FetchDataAndUpdate.cs  
          Starter funksjonalitet for å hente data fra API-ene og lagrer de i Azure SQL databasen
      - FetcherFunction.cs  
          Starter selve funksjonaliteten for å hente data og lagre dem i Azure SQL databasen 

    - DigitalTvillingKlima  
        Funksjonaliteter for å opprette digitale tvillinger basert på Meteorologisk institutt  
       - ApiDesc  
          Inneholder klasser for å dekonstruere API-et fra Meteorologisk institutt  
      - ApiInfo  
          Inneholder en klasse for å initialisere API-et som kommuniserer med Meteorologisk institutt  
      - DigitalTwin  
         En klasse som kjører logikken til å instansiere Api-ene, hente verdier fra Meteorologisk institutt api, opprette Meteorologisk institutt tvillinger og sende dem til Azure Digital Twin komponenten  
      - Hjelpeklasser  
          Inneholder klasser for å instansiere digitale tvilling klienten, sende json-modellene til Azure Digital Twins komponenten, bygge forhold med digitale tvillingene og opprette tvillinger med Meteorologisk institutt modellene  
      - DigitalTwinRun  
          En klasse som kjører logikken til den digitale tvillingen  
      - SykkelTvillingObjekter  
          Har flere klasser som etterligner Meteorologisk institutt modellene opprettet i Azure Digital Twin, som blir brukt for å opprette tvillinger   
      - DigitalTwinPublisher  
          Inneholder alle Azure function klassene   
    - DigitalTvillingSykkel  
       Funksjonaliteter for å opprette digitale tvillinger basert på Oslo Bysykkel  
      - ApiDesc  
          Inneholder klasser for å dekonstruere API-et brukt fra Oslo Bysykkel  
      - ApiInfo  
          Inneholder en klasse for å initialisere API-et som kommuniserer med Oslo Bysykkel  
      - DigitalTwin  
          Inneholder en klasse som er bygd opp for å opprette tvillinger med modellene ("Oslo Bysykkel modeller") lagt inn i Azure Digital Twin  
      - DigitalTwinRun  
          En klasse som kjører logikken til å instansiere Api-ene, hente verdier fra Oslo Bysykkel api, opprette Oslo bysykkel tvilling/er og sende dem til Azure Digital Twin komponenten  
      - SykkelTvillingObjekter  
          Inneholder klasser som etterligner modellene opprettet, som blir brukt for å opprette tvillinger   
      - DigitalTwinPublisher  
          Inneholder alle Azure function klassene   
    - Modeller/Tvilling_modeller  
        Her ligger modellene som ble brukt for å opprette den digitale tvillingen  

