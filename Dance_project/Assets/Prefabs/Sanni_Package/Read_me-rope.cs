// KÖYDEN TOIMINTA
// ensimmäinen bone > rigidbody > freeze position > kaikki päällä
// Jokaisessa bonessa hinge joint joissa
        // Spring = Lisää joustavuutta ja sitä ettei köysi ala säheltää ja täristä (suosittelen 100)
        // Damper = Pehmentää liikettä mutta saa köyden menemään rullalle (riippuu animaatiosta, 0-50?)
        // Ylempänä oleva luu laitetaan kohtaan "Connected body" (siihen laitettavalla objektilla tulee olla rigidbody)
//Viimeisessä bonessa 2 hingejointtia, joista toisessa Rope Target, eli asia jota köyden pää seuraa
// Rope Target laitetaan childiksi asialle jota köydenpään halutaan seuraavan.
//Huom. köysi toimii paremmin kun seurattava kohde (animaatio ei pyöri monta kertaa ympäri)