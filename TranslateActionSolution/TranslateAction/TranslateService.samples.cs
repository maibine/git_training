namespace TranslateAction
{
	internal partial class TranslateService
	{
		readonly static string sampleIt = @"---
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

È possibile modificare le priorità utilizzando i tasti **Sposta su**![](/img/neutral/common/move-up.png) e **Sposta Giù**![](/img/neutral/common/delete-cc.png).

*Valori di default*: il CDC o CDP viene recuperato dal tipo fatturato qualora presente. Per approfondimenti fare riferimento a tabella [Tipo Fatturato](/docs/configurations/tables/sales/sales-turnover).

Qualora non fosse presente nel *Tipo fatturato* il sistema lo cercherà in *Anagrafica cliente*. Se non presente verrà cercato in anagrafica articolo e successivamente a livello di magazzino.

<iframe width=""560"" height=""315"" src=""https://www.youtube.com/embed/WgkLl-D27io"" title=""YouTube video player"" frameborder=""0"" allow=""accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"" allowfullscreen></iframe>
";

		static readonly Dictionary<string, string> sampleItTranslations = new Dictionary<string, string>
	 {
		  { "ro", @"---
title: Parametri comenzi clienți (Parametri ordini clienti)
sidebar_position: 3
---

Parametrii comenzilor clienților permit configurarea de bază pentru a gestiona corect și conform cerințelor specifice ale fiecărei companii. Fereastra pentru acești parametri constă din 4 tab-uri diferite: General, Execuție, Descărcare și Analitic.

### General

**Propune comisioane pentru toate articolele: (Proponi provvigioni per tutti gli articoli:)** câmp devenit învechit, deoarece comisionul este întotdeauna gestionat independent de acest indicator;

**Plată (Pagamento)**: specifică valoarea utilizată pentru plata comenzii clientului în cazul execuției multiple a comenzii de vânzare: *Primă comandă (Primo ordine)*, *Registru (Anagrafica)* sau *Selecție manuală (Selezione manuale)*.

**Gestionare unitate de măsură dublă (Gestione doppia unità misura)**: acest indicator, dacă este activat, permite sistemului să gestioneze unitatea de măsură alternativă în comanda clientului; dacă nu este activat în comandă se va afișa doar unitatea de măsură de gestionare a articolului;

**Exclude comanda clientului (Escludi ordine cliente)**: dacă este activat, acest indicator indică faptul că angajamentul comenzilor clienților nu va fi luat în considerare în calculul disponibilității;

**Viz. disponibilitate (Vis. disponibilità)**: dacă este activat, datorită acestui indicator va exista posibilitatea de a vizualiza starea de epuizare la nivelul liniei de articol atunci când se introduce cantitatea. Condițiile pentru a primi mesajul că articolul este pe cale să se epuizeze sunt:         
- în parametrii Comenzilor clienților trebuie setat indicatorul ""Verifică articolele în epuizare"";          
- în [Registrul articolului] (/docs/erp-home/registers/items/create-new-items/create-new-item), tab-ul General, trebuie setat indicatorul ""În epuizare"" și, în plus, ""Data epuizării"" trebuie să fie mai mică sau egală cu data comenzii.

### Descărcare

**Prioritatea parametrilor introduși pentru fiecare linie a comenzii (Priorità parametri inseriti per ogni riga ordine)**: dacă este activat, depozitul și cauza de descărcare sunt citite din liniile comenzii, în timp ce dacă nu este activat, vor fi utilizate depozitul și cauza introduse în câmpurile următoare (*Depozit (Magazzino)* și *Cauza de depozitare (Causale di magazzino)*).

### Analitic

Acest TAB specifică cu ce prioritate să se recupereze Centrul de cost (CDC) sau Centrul de profit (CDP) în linia comenzii.

Este posibil să modificați prioritățile folosind butoanele **Mută în sus (Sposta su)**![](/img/neutral/common/move-up.png) și **Mută în jos (Sposta Giù)**![](/img/neutral/common/delete-cc.png).

*Valori implicite (Valori di default)*: CDC sau CDP este recuperat din tipul facturat dacă este prezent. Pentru informații detaliate, consultați tabelul [Tipul Facturat](/docs/configurations/tables/sales/sales-turnover).

Dacă nu este prezent în *Tipul facturat (Tipo fatturato)*, sistemul îl va căuta în *Registrul clientului (Anagrafica cliente)*. Dacă nu este prezent, va fi căutat în registrul articolului și ulterior la nivel de depozit.

<iframe width=""560"" height=""315"" src=""https://www.youtube.com/embed/WgkLl-D27io"" title=""Player video YouTube"" frameborder=""0"" allow=""accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"" allowfullscreen></iframe>"
	},  // Romanian
        //{ "es", "Adiós" },        // Spanish
        //{ "hr", "Doviđenja" }     // Croatian
    };
	}
}
