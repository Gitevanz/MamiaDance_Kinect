// K�YDEN TOIMINTA
// ensimm�inen bone > rigidbody > freeze position > kaikki p��ll�
// Jokaisessa bonessa hinge joint joissa
        // Spring = Lis�� joustavuutta ja sit� ettei k�ysi ala s�helt�� ja t�rist� (suosittelen 100)
        // Damper = Pehment�� liikett� mutta saa k�yden menem��n rullalle (riippuu animaatiosta, 0-50?)
        // Ylemp�n� oleva luu laitetaan kohtaan "Connected body" (siihen laitettavalla objektilla tulee olla rigidbody)
//Viimeisess� bonessa 2 hingejointtia, joista toisessa Rope Target, eli asia jota k�yden p�� seuraa
// Rope Target laitetaan childiksi asialle jota k�ydenp��n halutaan seuraavan.
//Huom. k�ysi toimii paremmin kun seurattava kohde (animaatio ei py�ri monta kertaa ymp�ri)