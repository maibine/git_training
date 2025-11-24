---
title: Parametri ordini clienti
sidebar_position: 3
---
 
I parametri degli ordini clienti permettono l'impostazione di base per gestire correttamente e secondo le specifiche richieste da ogni singola società. La finestra relativa a questi parametri si compone di 4 diversi tab: Generale, Evasione, Scarico e Analitica.
 
### Generale
 
**Proponi provvigioni per tutti gli articoli:** campo diventato obsoleto, in quanto la provvigione viene sempre gestita indipendentemente da questo flag;
**Pagamento**: specifica il valore usato per il pagamento dell'ordine cliente nel caso dell'evasione multipla commessa vendita: *Primo ordine*, *Anagrafica* oppure *Selezione manuale*.
**Gestione doppia unità misura**: questo flag, se attivo, consente al sistema di gestire l'unità di misura alternativa nell'ordine cliente; se non è attivo nell'ordine si vedrà riportata solamente l'unità di misura gestionale dell'articolo;
**Escludi ordine cliente**: se attivo, questo flag indica che l'impegno degli ordini clienti non verrà considerato nel calcolo della disponibilità;
**Vis. disponibilità**: se attivo, grazie a questo flag vi sarà la possibilità di visualizzare l'eventuale stato in esaurimento a livello di riga articolo quando si inserisce la quantità. Le condizioni per ottenere il messaggio che l'articolo sta per esaurire sono:         
- nei parametri Ordini cliente deve essere settato il flag ""Verifica articoli in esaurimento"";          
- in [Anagrafica articolo](/docs/erp-home/registers/items/create-new-items/create-new-item), tab Generalità, deve essere settato il flag ""In esaurimento"" e inoltre la ""Data esaurimento"" deve essere minore o uguale alla data dell'ordine.
 
### Scarico
 
**Priorità parametri inseriti per ogni riga ordine**: se attivo il magazzino e la causale di scarico vengono letti dalle righe dell'ordine mentre se non viene attivato, verranno utilizzati il magazzino e la causale inseriti nei campi successivi (*Magazzino* e *Causale di magazzino*).
 
### Analitica
 
Questo TAB specifica con che priorità recuperare il Centro di costo(CDC) o Centro di profitto(CDP) nella riga ordine.
È possibile modificare le priorità utilizzando i tasti **Sposta su**![](/img/neutral/common/move-up.png) e **Sposta Giù**![](/img/neutral/common/delete-cc.png).
*Valori di default*: il CDC o CDP viene recuperato dal tipo fatturato qualora presente. Per approfondimenti fare riferimento a tabella [Tipo Fatturato](/docs/configurations/tables/sales/sales-turnover).
Qualora non fosse presente nel *Tipo fatturato* il sistema lo cercherà in *Anagrafica cliente*. Se non presente verrà cercato in anagrafica articolo e successivamente a livello di magazzino.

## A cosa serve

La funzione degli **Ordini Pianificati** nel sistema Fluentis permette agli utenti di gestire la schedulazione della produzione.  
**La schedulazione generale** viene effettuata tramite la griglia di risultati della form specifica.  
Per accedere al collegamento tra i moduli dell'area, è necessario utilizzare il flag disponibile nella form.  
Il **conto lavoro** richiede la compilazione del campo conto lavoro nella griglia principale.  
Dopo l’**elaborazione MRP**, gli ordini evasi verranno automaticamente contrassegnati nella griglia.  
I rientri nella distinta base sono visibili nella form dedicata.  
*Data impiego* indica il momento in cui un ordine pianificato viene rilasciato nel sistema.  
Questa form viene utilizzata per verificare i dettagli degli ordini e la form si apre tramite il percorso specificato nella documentazione.  
 
<iframe width=""560"" height=""315"" src=""https://www.youtube.com/embed/WgkLl-D27io"" title=""YouTube video player"" frameborder=""0"" allow=""accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"" allowfullscreen></iframe>
