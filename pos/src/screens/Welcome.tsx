import { StyleSheet, Text, View } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import { Button } from 'react-native-paper';

const Welcome = () => {
  const navigation = useNavigation();
  return (
    <View>
      <Text>Welcome</Text>
      <Button onPress={() => navigation.navigate('PaymentScreen')}>
        Go Payment
      </Button>
    </View>
  );
};

export default Welcome;

const styles = StyleSheet.create({});
