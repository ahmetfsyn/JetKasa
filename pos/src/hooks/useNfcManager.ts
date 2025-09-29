import NfcManager, { NfcTech, Ndef } from 'react-native-nfc-manager';

export const useNfcManager = () => {
  async function readNdef() {
    try {
      await NfcManager.requestTechnology(NfcTech.Ndef);
      const tag = await NfcManager.getTag();
      const ndef = tag?.ndefMessage;

      if (ndef && ndef.length > 0) {
        // Payload decode
        const payload = ndef[0].payload; // genellikle ilk kayıt kullanılır
        const text = Ndef.text.decodePayload(payload); // Ndef.text.decodePayload ile stringe çevir
        const json = JSON.parse(text); // JSON olarak parse et

        return json;
      }
    } catch (ex) {
      console.warn('NFC Okuma Hatası', ex);
      throw ex;
    } finally {
      await NfcManager.cancelTechnologyRequest();
    }
  }

  return {
    readNdef,
  };
};
