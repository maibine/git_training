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
		  { "en", @"---
title: Customer Order Parameters (Parametri ordini clienti)
sidebar_position: 3
---

The parameters of customer orders allow for basic settings to manage correctly and according to the specific requests of each individual company. The window related to these parameters consists of 4 different tabs: General, Fulfillment, Shipping, and Analytical.

### General

**Propose commissions for all items: (Proponi provvigioni per tutti gli articoli:)** field that has become obsolete, as the commission is always managed independently of this flag;

**Payment (Pagamento)**: specifies the value used for the payment of the customer order in the case of multiple fulfillment of the sales order: *First order (Primo ordine)*, *Registry (Anagrafica)*, or *Manual selection (Selezione manuale)*.

**Management of double unit of measure (Gestione doppia unità misura)**: this flag, if active, allows the system to manage the alternative unit of measure in the customer order; if not active, only the management unit of measure of the item will be displayed in the order;

**Exclude customer order (Escludi ordine cliente)**: if active, this flag indicates that the commitment of customer orders will not be considered in the availability calculation;

**View availability (Vis. disponibilità)**: if active, thanks to this flag there will be the possibility to view the potential out-of-stock status at the item line level when entering the quantity. The conditions to receive the message that the item is about to run out are:         
- in the Customer Order parameters, the flag ""Check items in stock"" must be set;          
- in [Item Registry](/docs/erp-home/registers/items/create-new-items/create-new-item), General tab, the flag ""Out of stock"" must be set, and additionally, the ""Out of stock date"" must be less than or equal to the order date.

### Shipping

**Priority of parameters entered for each order line (Priorità parametri inseriti per ogni riga ordine)**: if active, the warehouse and the shipping reason are read from the order lines, while if not activated, the warehouse and the reason entered in the subsequent fields (*Warehouse (Magazzino)* and *Warehouse reason (Causale di magazzino)*).

### Analytical

This TAB specifies the priority for retrieving the Cost Center (CDC) or Profit Center (CDP) in the order line.

It is possible to modify the priorities using the buttons **Move Up (Sposta su)**![](/img/neutral/common/move-up.png) and **Move Down (Sposta Giù)**![](/img/neutral/common/delete-cc.png).

*Default values (Valori di default)*: the CDC or CDP is retrieved from the invoiced type if present. For further details, refer to the table [Invoiced Type](/docs/configurations/tables/sales/sales-turnover).

If it is not present in the *Invoiced Type (Tipo fatturato)*, the system will look for it in the *Customer Registry (Anagrafica cliente)*. If not present, it will be searched in the item registry and subsequently at the warehouse level.

<iframe width=""560"" height=""315"" src=""https://www.youtube.com/embed/WgkLl-D27io"" title=""YouTube video player"" frameborder=""0"" allow=""accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"" allowfullscreen></iframe>" 
			}, //english
		  { "fr", @"---
title: Paramètres des commandes clients (Parametri ordini clienti)
sidebar_position: 3
---

Les paramètres des commandes clients permettent de définir les réglages de base pour gérer correctement et selon les spécifications demandées par chaque entreprise. La fenêtre relative à ces paramètres se compose de 4 onglets différents : Général, Exécution, Déchargement et Analytique.

### Général 

**Proposer des commissions pour tous les articles: (Proponi provvigioni per tutti gli articoli:)** champ devenu obsolète, car la commission est toujours gérée indépendamment de ce drapeau ;

**Paiement (Pagamento)** : spécifie la valeur utilisée pour le paiement de la commande client en cas d'exécution multiple de la commande de vente : *Première commande (Primo ordine)*, *Fiche client (Anagrafica)* ou *Sélection manuelle (Selezione manuale)*.

**Gestion de l'unité de mesure double  (Gestione doppia unità misura)** : ce drapeau, s'il est actif, permet au système de gérer l'unité de mesure alternative dans la commande client ; s'il n'est pas actif, seule l'unité de mesure de gestion de l'article sera affichée dans la commande ;

**Exclure la commande client  (Escludi ordine cliente)** : si actif, ce drapeau indique que l'engagement des commandes clients ne sera pas pris en compte dans le calcul de la disponibilité ;

**Aff. disponibilité (Vis. disponibilità)** : si actif, grâce à ce drapeau, il sera possible de visualiser l'éventuel état d'épuisement au niveau de la ligne article lors de l'insertion de la quantité. Les conditions pour obtenir le message que l'article est sur le point de s'épuiser sont :         
- dans les paramètres Commandes clients, le drapeau ""Vérifier les articles en épuisement (Verifica articoli in esaurimento)"" doit être activé ;          
- dans [Fiche article](/docs/erp-home/registers/items/create-new-items/create-new-item), onglet Généralités (tab Generalità), le drapeau ""En épuisement (In esaurimento)"" doit être activé et de plus, la ""Date d'épuisement (Data esaurimento)"" doit être inférieure ou égale à la date de la commande.

### Déchargement 

**Priorité des paramètres saisis pour chaque ligne de commande (Priorità parametri inseriti per ogni riga ordine)** : si actif, l'entrepôt et la cause de déchargement sont lus à partir des lignes de la commande, tandis que s'il n'est pas activé, l'entrepôt et la cause saisis dans les champs suivants (*Entrepôt (Magazzino)* et *Cause de l'entrepôt (Causale di magazzino)*).

### Analytique 

Cet onglet spécifie avec quelle priorité récupérer le Centre de coût (CDC) ou le Centre de profit (CDP) dans la ligne de commande.

Il est possible de modifier les priorités en utilisant les boutons **Déplacer vers le haut (Sposta su)**![](/img/neutral/common/move-up.png) et **Déplacer vers le bas (Sposta Giù)**![](/img/neutral/common/delete-cc.png).

*Valeurs par défaut (Valori di default)* : le CDC ou CDP est récupéré à partir du type facturé s'il est présent. Pour plus de détails, veuillez vous référer au tableau [Type de facturé](/docs/configurations/tables/sales/sales-turnover).

S'il n'est pas présent dans le *Type de facturé (Tipo fatturato)*, le système le recherchera dans *Fiche client (Anagrafica cliente)*. S'il n'est pas présent, il sera recherché dans la fiche article, puis au niveau de l'entrepôt.

<iframe width=""""560"""" height=""""315"""" src=""""https://www.youtube.com/embed/WgkLl-D27io"""" title=""""YouTube video player"""" frameborder=""""0"""" allow=""""accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"""" allowfullscreen></iframe>" 
			}, //french
		  { "hr", @"---
title: Parametri narudžbi kupaca (Parametri ordini clienti)
sidebar_position: 3
---

Parametri narudžbi kupaca (I parametri degli ordini clienti) omogućuju osnovne postavke za pravilno upravljanje prema specifičnim zahtjevima svake pojedine tvrtke. Prozor vezan uz ove parametre sastoji se od 4 različite kartice: Općenito, Ispunjenje, Izdavanje i Analitika.

### Općenito (Generale)

**Predloži provizije za sve artikle: (Proponi provvigioni per tutti gli articoli:)** polje koje je postalo zastarjelo, budući da se provizija uvijek upravlja neovisno o ovoj oznaci;

**Plaćanje (Pagamento)**: specificira vrijednost koja se koristi za plaćanje narudžbe kupca u slučaju višestrukog ispunjenja prodajne narudžbe: *Prva narudžba (Primo ordine)*, *Osnovni podaci (Anagrafica)* ili *Ručno odabiranje (Selezione manuale)*.

**Upravljanje dvostrukom mjernom jedinicom (Gestione doppia unità misura)**: ova oznaka, ako je aktivna, omogućuje sustavu upravljanje alternativnom mjernom jedinicom u narudžbi kupca; ako nije aktivna, u narudžbi će biti prikazana samo upravna mjera artikla;

**Isključi narudžbu kupca (Escludi ordine cliente)**: ako je aktivna, ova oznaka označava da se obveza narudžbi kupaca neće uzeti u obzir u izračunu dostupnosti;

**Prik. dostupnost (Vis. disponibilità)**: ako je aktivna, zahvaljujući ovoj oznaci bit će moguće vidjeti eventualno stanje u iscrpljivanju na razini stavke artikla kada se unosi količina. Uvjeti za dobivanje poruke da je artikl na izdisaju su:         
- u parametrima Narudžbe kupaca mora biti postavljena oznaka ""Provjeri artikle u iscrpljivanju (Verifica articoli in esaurimento)"";          
- u [Osnovni podaci o artiklu](/docs/erp-home/registers/items/create-new-items/create-new-item), kartica Općenito (tab Generalità), mora biti postavljena oznaka ""Na izdisaju (In esaurimento)"" i osim toga ""Datum iscrpljenja (Data esaurimento)"" mora biti manji ili jednak datumu narudžbe.

### Izdavanje (Scarico)

**Prioritet parametara unesenih za svaki redak narudžbe (Priorità parametri inseriti per ogni riga ordine)**: ako je aktivna, skladište i razlog izdavanja se čitaju iz redaka narudžbe, dok ako nije aktivna, koristiti će se skladište i razlog uneseni u sljedećim poljima (*Skladište (Magazzino)* i *Razlog skladišta (Causale di magazzino)*).

### Analitika (Analitica)

Ova kartica specificira s kojim prioritetom preuzeti Centar troška (CDC) ili Centar profita (CDP) u retku narudžbe.

Moguće je promijeniti prioritete koristeći tipke **Pomakni gore (Sposta su)**![](/img/neutral/common/move-up.png) i **Pomakni dolje (Sposta Giù)**![](/img/neutral/common/delete-cc.png).

*Zadane vrijednosti (Valori di default)*: CDC ili CDP se preuzima iz tipa fakturiranog ako je prisutan. Za više informacija pogledajte tablicu [Tip fakturiranog (Tipo Fatturato)](/docs/configurations/tables/sales/sales-turnover).

Ako nije prisutan u *Tipu fakturiranog (Tipo fatturato)*, sustav će ga tražiti u *Osnovnim podacima o kupcu (Anagrafica cliente)*. Ako nije prisutan, tražit će se u osnovnim podacima o artiklu, a zatim na razini skladišta.

<iframe width=""""560"""" height=""""315"""" src=""""https://www.youtube.com/embed/WgkLl-D27io"""" title=""""YouTube video player"""" frameborder=""""0"""" allow=""""accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture"""" allowfullscreen></iframe>" }, //Craotian
		  //{ "cn", @"" },
			//{ "pt", @"" },
	 };
	}
}
