import Welcome from '../screens/Welcome';
import Payment from '../screens/Payment';
import { createStackNavigator } from '@react-navigation/stack';
const Stack = createStackNavigator();

const AppNavigator = () => {
  return (
    <Stack.Navigator
      screenOptions={{
        animation: 'default',
        headerShown: false,
      }}
    >
      <Stack.Screen name="WelcomeScreen" component={Welcome} />
      <Stack.Screen name="PaymentScreen" component={Payment} />
    </Stack.Navigator>
  );
};

export default AppNavigator;
